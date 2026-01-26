
$time = (Get-Date).ToString("yyyyMMdd_HHmmss")
$FileName = "0000_All_Script_" + $time + ".sql"

cat *.sql | sc $FileName
