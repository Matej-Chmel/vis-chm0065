$xml = [IO.Path]::Combine($PSScriptRoot, "databases", "XML")
$backup = [IO.Path]::Combine($xml, "backup", "*.xml")
$xmlFiles = [IO.Path]::Combine($xml, "*.xml")

Remove-Item $xmlFiles
Copy-Item $backup $xml
Write-Host "XML database was restored successfully."
Stop-Process -Id $PID
