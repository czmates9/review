$ErrorActionPreference = 'Stop'
$root = $PSScriptRoot
$output = Join-Path $root '0x_Zdrojove_Kody\!Build!\Konzola\Debug'
[void][Reflection.Assembly]::LoadFrom((Join-Path $output 'Fask.Encryption.dll'))
$assembly = [Reflection.Assembly]::LoadFrom((Join-Path $output 'Konzola.exe'))
[void][Konzola.Konfigurace.Globals_Konfig_Konzola]::LoadConfiguration()
$config = [Konzola.Konfigurace.Globals_Konfig_Konzola]::Konfigurace
$config.System[0].FASKDB_ConnesctionString = 'Data Source=localhost;Initial Catalog=FASK;Integrated Security=True;Application Name=Konzola Debug;Connect Timeout=5'
$config.System[0].Pracovnici_synchronizaceAD = $false
$config.Provider[0].ProviderKonzola = 'Fask.ModuleSql.dll'
$config.Ostatni[0].ScannerType = 'None'
$result = [Konzola.Konfigurace.Globals_Konfig_Konzola]::SaveConfiguration()
if ($result -ne 'OK') { throw $result }
$licensePath = Join-Path $output 'konzola.ini'
if (Test-Path $licensePath) { Copy-Item $licensePath ($licensePath + '.backup-' + (Get-Date -Format yyyyMMddHHmmss)) }
$doc = New-Object Xml.XmlDocument
[void]$doc.AppendChild($doc.CreateXmlDeclaration('1.0','utf-8',$null))
$license = $doc.CreateElement('License')
[void]$doc.AppendChild($license)
$values = [ordered]@{ company='FASK - LOCAL DEBUG'; contact='Local development'; created=(Get-Date -Format dd.MM.yyyy); expiration=(Get-Date).AddYears(1).ToString('dd.MM.yyyy') }
$source = Get-Content (Join-Path $root '0x_Zdrojove_Kody\Konzola\Konzola\Licence\Licensing.cs') -Raw
foreach ($match in [regex]::Matches($source, 'XmlNodeList\s+\w+\s*=\s*doc.GetElementsByTagName\("([^"]+)"\)')) {
    $name = $match.Groups[1].Value
    if (!$values.Contains($name)) { $values[$name] = 'true' }
}
foreach ($name in $values.Keys) { $node=$doc.CreateElement($name); $node.InnerText=$values[$name]; [void]$license.AppendChild($node) }
$doc.Save($licensePath)
$password = $assembly.GetType('Konzola.Licence.Licensing').GetField('licensepassword',[Reflection.BindingFlags]'NonPublic,Static').GetRawConstantValue()
$signature = [Fask.Encryption.RijndaelWrapper]::Encrypt([IO.File]::ReadAllText($licensePath),$password)
$node = $doc.CreateElement('Authority'); $node.InnerText=$signature; [void]$license.AppendChild($node)
$doc.Save($licensePath)
$validation = New-Object Konzola.Licence.Licensing
if (!$validation.IsLicensed -or $validation.Status -ne 'OK') { throw ('License check: ' + $validation.Status) }
Write-Output ('License validated; expiration: ' + $values.expiration)
Write-Output ('Debug directory: ' + $output)
