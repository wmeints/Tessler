Function CheckExitCode($message) {
	if ($LASTEXITCODE -ne 0) {
		Write-Host "$message ended with code $LASTEXITCODE" -f red
		Exit 1
	}
}

Function Green($message) {
	Write-Host $message -f green
}

Function Red($message) {
	Write-Host $message -f red
}

Function Yellow($message) {
	Write-Host $message -f yellow
}

Function Header($message) {
	Write-Host $message -f magenta
}

Function WillWeRun($taskName, $willRun) {
	if ($willRun -eq $true) {
		Green "$($taskName) YES"
	} else {
		Red "$($taskName) NO"
	}
}

Function CheckFile($file) {
	Write-Host "Checking existence of '$file'..." -f yellow
	$filename = [System.IO.Path]::GetFileName($file)
	if (Test-Path $file) {
		Green "$filename was found"
	} else {
		Write-Host "$file was not found" -f red
		Exit 1
	}
}

Function Update-SourceVersion($file, $version)
{
	$newVersion = 'AssemblyVersion("' + $version + '")';
	$newFileVersion = 'AssemblyFileVersion("' + $version + '")';

	Write-Host "Updating $file..."
	$tmpFile = $file + ".tmp"

	get-content $file | 
	%{$_ -replace 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', $newVersion } |
	%{$_ -replace 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', $newFileVersion }  > $tmpFile

	move-item $tmpFile $file -force
}

Function Update-AllAssemblyInfoFiles($rootFolder, $version)
{
	$files = Get-ChildItem -Path $rootFolder -Recurse -Filter "AssemblyInfo.cs"
	foreach($file in $files)
	{
		Update-SourceVersion $file.FullName $version
	}	
}