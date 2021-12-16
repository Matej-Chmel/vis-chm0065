param(
	[switch] $Restore = $false
)

function NextAttempt {
	param(
		[int] $attempt
	)

	Write-Host "Attempt $attempt failed. Retrying in 500 ms."
	Start-Sleep -Milliseconds 500
	return $attempt + 1
}

$databases = [IO.Path]::Combine($PSScriptRoot, "databases")
$status = [IO.Path]::Combine($databases, "MariaDBStatus.txt")
$bin = [IO.Path]::Combine($databases, "MariaDB", "bin")
$mysqld = [IO.Path]::Combine($bin, "mysqld")

if(!(Test-Path $status)) {
	$archive = [IO.Path]::Combine($databases, "MariaDB.zip")
	Expand-Archive $archive -DestinationPath $databases

	$install = [IO.Path]::Combine($bin, "mysql_install_db")
	& $install

	$Restore = $true
	$rootMissing = $true
}

Start-Process $mysqld -ArgumentList "--console"

if($rootMissing) {
	$admin = [IO.Path]::Combine($bin, "mysqladmin")
	$attempt = 1
	Write-Host "Trying to create root user. Please wait."

	while($attempt -le 240) {
		& $admin -u root flush-privileges password "VISchm0065DB"

		if($?) {
			Write-Host "Success."
			Write-Output "Installed." > $status
			break
		}

		$attempt = NextAttempt $attempt
	}
}

if($Restore) {
	$mysql = [IO.Path]::Combine($bin, "mysql")
	$schema = [IO.Path]::Combine($databases, "schema.sql")

	$attempt = 1
	Write-Host "Trying to restore the database. Please wait."

	while($attempt -le 240) {
		& $mysql --user=root --password=VISchm0065DB --execute="source $schema"

		if($?) {
			Write-Host "Success."
			break
		}

		$attempt = NextAttempt $attempt
	}
}

Stop-Process -Id $PID
