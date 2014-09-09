REM Create a single dll to add in the NuGet package

rmdir /s /q "Merged"
mkdir "Merged"
ILRepack.exe /target:library /out:"Merged\InfoSupport.Tessler.dll" ^
 InfoSupport.Tessler.dll ^
 log4net.dll ^
 Microsoft.Practices.ServiceLocation.dll ^
 Microsoft.Practices.Unity.Configuration.dll ^
 Microsoft.Practices.Unity.dll ^
 Microsoft.Practices.Unity.Interception.Configuration.dll ^
 Microsoft.Practices.Unity.Interception.dll ^
 Moq.dll ^
 WebDriver.dll ^
 WebDriver.Support.dll