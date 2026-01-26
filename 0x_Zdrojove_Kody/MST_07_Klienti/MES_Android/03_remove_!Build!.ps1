Get-ChildItem .\ -include !Build!,!!!Build!!! -Recurse -Force | foreach ($_) { remove-item $_.fullname -Force -Recurse -Verbose }
