Get-ChildItem .\ -include *.user -Recurse -Force | foreach ($_) { remove-item $_.fullname -Force -Recurse -Verbose }
