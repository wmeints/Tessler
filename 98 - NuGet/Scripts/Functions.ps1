# Functions

Function CheckExitCode($message) {
	if ($LASTEXITCODE -ne 0) {
		Write-Host "$message ended with code $LASTEXITCODE" -f red
		Quit
	}
}

Function Green($message) {
	Write-Host $message -f green
}

Function CheckFile($file) {
	$filename = [System.IO.Path]::GetFileName($file)
	if (Test-Path $file) {
		Green "$filename was found"
	} else {
		Write-Host "$file was not found" -f red
		Quit
	}
}

Function Quit {
	Read-Host
	Exit
}