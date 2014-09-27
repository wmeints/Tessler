param(
	[switch]$updateVersion = $false,
	[switch]$build = $false,
	[switch]$runUnitTests = $false,
	[switch]$runUITests = $false,
	[switch]$createNuGetPackage = $false,
	[switch]$publishNuGetPackage = $false,
	[switch]$verbose = $false
)

$here = Split-Path -Parent $MyInvocation.MyCommand.Definition
$root = (Get-Item $here).Parent.FullName

. "$here\Functions.ps1"

Yellow "Working directory: $here"
Yellow "Root directory: $root"

WillWeRun "Update version          " $updateVersion
WillWeRun "Build                   " $build
WillWeRun "Run unit tests          " $runUnitTests
WillWeRun "Run UI tests            " $runUITests
WillWeRun "Create NuGet package    " $createNuGetPackage
WillWeRun "Publish NuGet package   " $publishNuGetPackage

# Platform tools
$msbuild = "C:\Windows\Microsoft.Net\Framework\v4.0.30319\MSBuild.exe"
$vstest = "C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe"
$nuget = "$here\..\01 - Tessler\.nuget\NuGet.exe"

# Project variables
$unittestdll = "$here\..\01 - Tessler\Tessler.UnitTest\bin\Release\InfoSupport.Tessler.UnitTest.dll"
$uitestdll = "$here\..\01 - Tessler\Tessler.UITest\bin\Release\InfoSupport.Tessler.UITest.dll"
$versionfile = "version.txt"
$releasefolder = "Release"

# Determine version
if (Test-Path $versionfile) {
	$version = Get-Content $versionfile
	Write-Host "Version file found, using $version" -f yellow
} else {
	Write-Host "No version file found, using 1.0.0" -f yellow
	$version = "1.0.0"
}

# Update version
if ($updateVersion -eq $true) {
	Header "Updating version numbers to $version..."
	Update-AllAssemblyInfoFiles $root $version
	Green "Version update successful"
} else {
	Write-Host "Skipping version update" -f yellow
}

# Build
if ($build -eq $true) {
	Header "Building Tessler..."
	CheckFile $msbuild
	& $msbuild "$here\..\01 - Tessler\Tessler.sln" /p:Configuration=Release
	CheckExitCode "Build"
	Green "Buid successful"
} else {
	Write-Host "Skipping build" -f yellow
}

# Unit tests
if ($runUnitTests -eq $true) {
	Header "Running unit tests..."
	CheckFile $vstest
	& $vstest $unittestdll
	CheckExitCode "Unit tests"
	Green "Unit test run successful"
} else {
	Write-Host "Skipping unit tests" -f yellow
}

# UI tests
if ($runUITests -eq $true) {
	Header "Running UI tests..."
	CheckFile $vstest
	& $vstest $uitestdll
	CheckExitCode "UI tests"
	Green "UI test run successful"
} else {
	Write-Host "Skipping UI tests" -f yellow
}

# Create NuGet package
if ($createNuGetPackage -eq $true) {
	Header "Updating NuGet..."
	CheckFile $nuget
	& $nuget Update -self
	CheckExitCode "NuGet Update"
	
	Header "Creating Tessler NuGet package..."
	& $nuget Pack "Package\tessler.nuspec" -OutputDirectory "Release" -Version $version -BasePath $root
	& $nuget Pack "Package\tessler.specflow.nuspec" -OutputDirectory "Release" -Version $version -BasePath $root
	CheckExitCode "NuGet Pack"
} else {
	Write-Host "Skipping creation of NuGet package" -f yellow
}

# Publish NuGet package
if ($publishNuGetPackage -eq $true) {
	Header "Publishing NuGet package..."
	CheckFile $nuget
	if ((File-Exists $apikeyfile) -ne $true) {
		Write-Host "Could not publish to NuGet, no api key available" -f red
		Exit 1
	}
	
	$apiKey = Get-Content $apikeyfile
	
	& $nuget SetApiKey $apiKey -Source $nugetUrl
	
	& $nuget Push "$releasefolder\Tessler.$version.nupkg"
	& $nuget Push "$releasefolder\Tessler.SpecFlow.$version.nupkg"

	CheckExitCode "NuGet Publish"
} else {
	Write-Host "Skipping publish to NuGet.org"
}

Green "Done"