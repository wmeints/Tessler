REM Create a single dll to add in the NuGet package

rmdir /s /q "Merged"
mkdir "Merged"
ILRepack.exe /target:library /out:"Merged\TesslerGeneratorProvider.Generator.SpecFlowPlugin.dll" ^
 TesslerGeneratorProvider.Generator.SpecFlowPlugin.dll ^
 TechTalk.SpecFlow.Generator.dll ^
 TechTalk.SpecFlow.Parser.dll ^
 TechTalk.SpecFlow.Utils.dll ^
 Gherkin.dll ^
 IKVM.OpenJDK.Core.dll ^
 IKVM.OpenJDK.Security.dll ^
 IKVM.OpenJDK.Text.dll ^
 IKVM.OpenJDK.Util.dll ^
 IKVM.Runtime.dll