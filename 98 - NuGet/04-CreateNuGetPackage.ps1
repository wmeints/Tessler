$ErrorActionPreference = "Stop"

Write-Host "Creating Tessler NuGet package..." -f magenta

$here = Split-Path -Parent $MyInvocation.MyCommand.Definition
$root = (Get-Item $here).Parent.FullName

. "$here\10-Functions.ps1"

# Project variables
Write-Host "Checking required files..." -f yellow

$versionfile = "$here\version.txt"
$releasefolder = "$here\Release"

CheckFile $nuget
CheckFile $versionfile
CheckFile $releaseFolder

# Set version
if (Test-Path $versionfile) {
	$version = Get-Content $versionfile
	Write-Host "Version file found, using $version" -f yellow
} else {
	Write-Host "No version file found, using 1.0.0" -f yellow
	$version = "1.0.0"
}

Green "Updating NuGet..." 
& $nuget Update -self
CheckExitCode "NuGet Update"

& $nuget Pack "$here\Package\tessler.nuspec" -OutputDirectory $releasefolder -Version $version -BasePath $root
& $nuget Pack "$here\Package\tessler.specflow.nuspec" -OutputDirectory $releasefolder -Version $version -BasePath $root
CheckExitCode "NuGet Pack"

Write-Host "Done" -f green
if ($autoExit -ne $true) {
	Quit
}