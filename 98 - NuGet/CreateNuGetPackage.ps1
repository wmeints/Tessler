param(
	[switch]$skipBuild,
	[switch]$skipUnitTests,
	[switch]$skipUITests,
	[switch]$skipCopyBinaries,
	[switch]$skipNuGetPackage,
	[switch]$verbose,
	[switch]$autoExit
)

. ".\Scripts\Functions.ps1"

Write-Host "Creating Tessler NuGet package..."

# Platform tools
$msbuild = "C:\Windows\Microsoft.Net\Framework\v4.0.30319\MSBuild.exe"
$vstest = "C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"
$nuget = "..\01 - Tessler\.nuget\NuGet.exe"
$here = Split-Path -Parent $MyInvocation.MyCommand.Definition
$root = (Get-Item $here).Parent.FullName

# Project variables
$unittestdll = "..\01 - Tessler\Tessler.UnitTest\bin\Release\InfoSupport.Tessler.UnitTest.dll"
$uitestdll = "..\01 - Tessler\Tessler.UITest\bin\Release\Tessler.UITest.dll"
$versionfile = "version.txt"

$binaries = @{
	#"..\01 - Tessler\Tessler\bin\Release\InfoSupport.Tessler.dll" = "Package\lib\InfoSupport.Tessler.dll"
	#"..\01 - Tessler\Tessler\bin\Release\WebDriver.dll" = "Package\lib\WebDriver.dll"
}

CheckFile $msbuild
CheckFile $vstest
CheckFile $nuget

if ($skipBuild -ne $true) {
	Green "Building Tessler..."
	& $msbuild "..\01 - Tessler\Tessler.sln" /p:Configuration=Release
	CheckExitCode "Build"
	Green "Buid successful"
} else {
	Write-Host "Skipping build" -f yellow
}

if ($skipUnitTests -ne $true) {
	Green "Running unit tests..."
	& $vstest $unittestdll
	CheckExitCode "Unit tests"
	Green "Unit test run successful"
} else {
	Write-Host "Skipping unit tests" -f yellow
}

if ($skipUITests -ne $true) {
	Green "Running UI tests..."
	& $vstest $uitestdll
	CheckExitCode "UI tests"
	Green "UI test run successful"
} else {
	Write-Host "Skipping UI tests" -f yellow
}

if ($skipCopyBinaries -ne $true) {
	Green "Copying package binaries..."
		$binaries.Keys | % {
			try {
				$filename = [System.IO.Path]::GetFileName($_)
				if ($verbose) { Write-Host "Copying $filename..." -f yellow }
				Copy-Item $_ $binaries.Item($_) -ErrorAction Stop
			}
			catch {
				Write-Host "Error copying binaries: $_" -f red
				Quit
			}
		}
} else {
	Write-Host "Skipping copying of binaries" -f yellow
}

if ($skipNuGetPackage -ne $true) {
	Green "Updating NuGet..." 
	& $nuget Update -self
	CheckExitCode "NuGet Update"
	
	if (Test-Path $versionfile) {
		$version = Get-Content $versionfile
	} else {
		Write-Host "No version file found, using 1.0.0" -f yellow
		$version = "1.0.0"
	}
	
	& $nuget Pack "Package\tessler.nuspec" -OutputDirectory "Release" -Version $version -BasePath $root
	& $nuget Pack "Package\tessler.specflow.nuspec" -OutputDirectory "Release" -Version $version -BasePath $root
	CheckExitCode "NuGet Pack"
	
	$doPublish = Read-Host "Publish to NuGet.org? (y/N)"
		
	if ($doPublish -eq "y")
	{
		#$apiKey = Get-Content $apikeyfile
		
		& $nuget SetApiKey "b21a1f7a-bde6-341c-a0dc-885f8fb4bc91"
		
		& $nuget Push "$releasefolder\Storm.$version.nupkg"
		& $nuget Push "$releasefolder\Storm.Dapper.$version.nupkg"

		CheckExitCode "NuGet Push"
	} else {
		Write-Host "Skipping publishment to NuGet.org"
	}
} else {
	Write-Host "Skipping creation of NuGet package" -f yellow
}

Write-Host "Done" -f green
if ($autoExit -ne $true) {
	Quit
}