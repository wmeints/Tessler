powershell ./CreateNuGetPackage.ps1 ^
	-updateVersion ^
	-build ^
	-runUnitTest ^
	-runUITest ^
	-createNuGetPackage ^
	| wtee CreateNuGetPackage.log