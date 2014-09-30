powershell ./CreateNuGetPackage.ps1 ^
	-build ^
	-runUnitTest ^
	-runUITest ^
	-createNuGetPackage ^
	| wtee CreateNuGetPackage.log