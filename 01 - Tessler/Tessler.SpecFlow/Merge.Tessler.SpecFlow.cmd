ECHO Create a single dll to add in the NuGet package

IF EXIST "Merged.Tessler.SpecFlow" rmdir /s /q "Merged.Tessler.SpecFlow"
mkdir "Merged.Tessler.SpecFlow"

ILRepack.exe /target:library /out:"Merged.Tessler.SpecFlow\TesslerGeneratorProvider.Generator.SpecFlowPlugin.dll" ^
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