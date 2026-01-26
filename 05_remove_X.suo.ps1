Get-ChildItem .\ -include *.suo -Recurse -Force | foreach ($_) { remove-item $_.fullname -Force -Recurse -Verbose }
