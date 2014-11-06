powershell ./CreateNuGetPackage.ps1 ^
	-build ^
	-runUnitTest ^
	-runUITest ^
	-createNuGetPackage ^
	-version "1.0.0.2"
	| wtee CreateNuGetPackage.log