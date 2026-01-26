Get-ChildItem .\ -include .vs -Recurse -Force | foreach ($_) { remove-item $_.fullname -Force -Recurse -Verbose }
