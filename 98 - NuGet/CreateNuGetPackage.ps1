param(
	[switch]$build = $false,
	[switch]$runUnitTests = $false,
	[switch]$runUITests = $false,
	[switch]$createNuGetPackage = $false,
	[switch]$publishNuGetPackage = $false,
	[switch]$verbose = $false,
	[string]$version = $null,
	[string]$apikey = $null
)

$here = Split-Path -Parent $MyInvocation.MyCommand.Definition
$root = (Get-Item $here).Parent.FullName

. "$here\Functions.ps1"

Yellow "Working directory: $here"
Yellow "Root directory: $root"

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
$releasefolder = "Release"

# Update version
if ($version -ne $null) {
	Header "Updating version numbers to $version..."
	Update-AllAssemblyInfoFiles $root $version
	Green "Version update successful"
} else {
	Write-Host "No version specified, skip creating and publishing NuGet package" -f yellow
	$createNuGetPackage = $false
	$publishNuGetPackage = $false
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
	& $nuget Pack "$here\Package\tessler.nuspec" -OutputDirectory "$here\$releaseFolder" -Version $version -BasePath $root
	& $nuget Pack "$here\Package\tessler.specflow.nuspec" -OutputDirectory "$here\$releaseFolder" -Version $version -BasePath $root
	CheckExitCode "NuGet Pack"
} else {
	Write-Host "Skipping creation of NuGet package" -f yellow
}

# Publish NuGet package
if ($publishNuGetPackage -eq $true) {
	Header "Publishing NuGet package..."
	CheckFile $nuget
	if ($apikey -eq $null) {
		Write-Host "Could not publish to NuGet, no api key available" -f red
		Exit 1
	}
	
	& $nuget SetApiKey $apikey
	
	& $nuget Push "$here\$releasefolder\Tessler.$version.nupkg"
	& $nuget Push "$here\$releasefolder\Tessler.SpecFlow.$version.nupkg"

	CheckExitCode "NuGet Publish"
} else {
	Write-Host "Skipping publish to NuGet.org"
}

Green "Done"