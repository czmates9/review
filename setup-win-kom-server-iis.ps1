[CmdletBinding()]
param()

$ErrorActionPreference = 'Stop'

$principal = New-Object Security.Principal.WindowsPrincipal([Security.Principal.WindowsIdentity]::GetCurrent())
if (-not $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)) {
    Write-Host 'Pro nastaveni IIS je potreba potvrdit dialog Rizeni uzivatelskych uctu (UAC).'
    $elevated = Start-Process -FilePath 'powershell.exe' -Verb RunAs -Wait -PassThru -ArgumentList @(
        '-NoProfile',
        '-ExecutionPolicy', 'Bypass',
        '-File', "`"$PSCommandPath`""
    )
    exit $elevated.ExitCode
}

$repositoryRoot = Split-Path -Parent $PSCommandPath
$sourcePath = Join-Path $repositoryRoot '0x_Zdrojove_Kody\Win_Kom_Server\MST_Win_Kom_Server'
$deployPath = 'C:\inetpub\FASK\MST_W_Server'
$siteName = 'MST_W_Server'
$appPoolName = 'MST_W_Server'
$port = 8000

if (-not (Test-Path -LiteralPath (Join-Path $sourcePath 'Web.config'))) {
    throw "Zdrojovy Web.config nebyl nalezen v $sourcePath"
}

$resolvedDeployPath = [IO.Path]::GetFullPath($deployPath).TrimEnd('\')
if ($resolvedDeployPath -ne 'C:\inetpub\FASK\MST_W_Server') {
    throw "Neocekavana cilova cesta nasazeni: $resolvedDeployPath"
}

$features = @(
    'IIS-WebServerRole',
    'IIS-WebServer',
    'IIS-CommonHttpFeatures',
    'IIS-StaticContent',
    'IIS-DefaultDocument',
    'IIS-HttpErrors',
    'IIS-ApplicationDevelopment',
    'IIS-NetFxExtensibility45',
    'IIS-ASPNET45',
    'IIS-ISAPIExtensions',
    'IIS-ISAPIFilter',
    'IIS-ManagementConsole'
)

foreach ($feature in $features) {
    $state = Get-WindowsOptionalFeature -Online -FeatureName $feature
    if ($state.State -ne 'Enabled') {
        Write-Host "Zapinam soucast Windows: $feature"
        Enable-WindowsOptionalFeature -Online -FeatureName $feature -All -NoRestart | Out-Null
    }
}

Import-Module WebAdministration

$conflictingBinding = Get-WebBinding -Protocol 'http' -Port $port -ErrorAction SilentlyContinue |
    Where-Object { $_.ItemXPath -notmatch "site\[@name='$([regex]::Escape($siteName))'" }
if ($conflictingBinding) {
    $owners = $conflictingBinding | ForEach-Object { $_.ItemXPath }
    throw "Port $port uz pouziva jiny IIS web: $($owners -join ', ')"
}

if (Test-Path "IIS:\Sites\$siteName") {
    Stop-Website -Name $siteName -ErrorAction SilentlyContinue
}

New-Item -ItemType Directory -Path $deployPath -Force | Out-Null

Write-Host "Kopiruji web do $deployPath"
& robocopy.exe $sourcePath $deployPath /E /R:2 /W:1 /NFL /NDL /NJH /NJS /NP /XD '.vs' 'obj' 'packages' /XF '*.csproj.user' '*.suo'
if ($LASTEXITCODE -ge 8) {
    throw "Kopirovani webu selhalo. Robocopy exit code: $LASTEXITCODE"
}

if (-not (Test-Path "IIS:\AppPools\$appPoolName")) {
    New-WebAppPool -Name $appPoolName | Out-Null
}

Set-ItemProperty "IIS:\AppPools\$appPoolName" -Name managedRuntimeVersion -Value 'v4.0'
Set-ItemProperty "IIS:\AppPools\$appPoolName" -Name managedPipelineMode -Value 'Integrated'
Set-ItemProperty "IIS:\AppPools\$appPoolName" -Name enable32BitAppOnWin64 -Value $false
Set-ItemProperty "IIS:\AppPools\$appPoolName" -Name startMode -Value 'AlwaysRunning'
Set-ItemProperty "IIS:\AppPools\$appPoolName" -Name processModel.identityType -Value 'ApplicationPoolIdentity'
Set-ItemProperty "IIS:\AppPools\$appPoolName" -Name processModel.idleTimeout -Value ([TimeSpan]::Zero)

if (-not (Test-Path "IIS:\Sites\$siteName")) {
    New-Website -Name $siteName -Port $port -IPAddress '*' -PhysicalPath $deployPath -ApplicationPool $appPoolName | Out-Null
} else {
    Set-ItemProperty "IIS:\Sites\$siteName" -Name physicalPath -Value $deployPath
    Set-ItemProperty "IIS:\Sites\$siteName" -Name applicationPool -Value $appPoolName
    Get-WebBinding -Name $siteName | Remove-WebBinding
    New-WebBinding -Name $siteName -Protocol 'http' -IPAddress '*' -Port $port
}

Set-ItemProperty "IIS:\Sites\$siteName" -Name serverAutoStart -Value $true
Set-WebConfigurationProperty -PSPath 'MACHINE/WEBROOT/APPHOST' -Location $siteName -Filter 'system.applicationHost/sites/site/application' -Name preloadEnabled -Value $true

& icacls.exe $deployPath /grant "IIS AppPool\$appPoolName`:(OI)(CI)M" /T /C /Q | Out-Null
if ($LASTEXITCODE -ne 0) {
    throw "Nastaveni opravneni pro IIS AppPool\$appPoolName selhalo."
}

$sqlScript = Join-Path $repositoryRoot 'setup-fask-iis-login.sql'
& sqlcmd.exe -S localhost -E -b -i $sqlScript
if ($LASTEXITCODE -ne 0) {
    throw 'Nepodarilo se nastavit SQL opravneni aplikačního poolu.'
}

Set-Service -Name W3SVC -StartupType Automatic
Start-Service -Name W3SVC
Start-WebAppPool -Name $appPoolName
Start-Website -Name $siteName

Write-Host ''
Write-Host "Hotovo: http://localhost:$port/"
Write-Host "IIS web: $siteName"
Write-Host "Application pool: $appPoolName"
Write-Host "Fyzicka cesta: $deployPath"

