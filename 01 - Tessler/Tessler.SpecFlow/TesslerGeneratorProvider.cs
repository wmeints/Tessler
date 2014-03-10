using TechTalk.SpecFlow.Infrastructure;

[assembly: GeneratorPlugin(typeof(TesslerGeneratorProvider.Generator.SpecFlowPlugin.TesslerGeneratorPlugin))]

namespace TesslerGeneratorProvider.Generator.SpecFlowPlugin
{
    using System.CodeDom;
    using System.Linq;

    using BoDi;

    using TechTalk.SpecFlow.Generator;
    using TechTalk.SpecFlow.Generator.Configuration;
    using TechTalk.SpecFlow.Generator.Plugins;
    using TechTalk.SpecFlow.Generator.UnitTestProvider;
    using TechTalk.SpecFlow.Parser.SyntaxElements;
    using TechTalk.SpecFlow.UnitTestProvider;
    using TechTalk.SpecFlow.Utils;

    using InfoSupport.Tessler.Util;
    using InfoSupport.Tessler.SpecFlow;

    /// <summary>
    /// The CodedUI generator plugin.
    /// </summary>
    public class TesslerGeneratorPlugin : IGeneratorPlugin
    {
        /// <summary>
        /// The register dependencies.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public void RegisterDependencies(ObjectContainer container)
        {
        }

        /// <summary>
        /// The register customizations.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <param name="generatorConfiguration">
        /// The generator configuration.
        /// </param>
        public void RegisterCustomizations(ObjectContainer container, SpecFlowProjectConfiguration generatorConfiguration)
        {
            container.RegisterTypeAs<TesslerGeneratorProvider, IUnitTestGeneratorProvider>();
            container.RegisterTypeAs<MsTest2010RuntimeProvider, IUnitTestRuntimeProvider>();
        }

        /// <summary>
        /// The register configuration defaults.
        /// </summary>
        /// <param name="specFlowConfiguration">
        /// The spec flow configuration.
        /// </param>
        public void RegisterConfigurationDefaults(SpecFlowProjectConfiguration specFlowConfiguration)
        {
        }
    }

    /// <summary>
    /// Extension for the SpecFlow MsTest2010GeneratorProvider. 
    /// This extension adds possibility to use the ResetDatabase tag within the Specflow feature file.
    /// A ResetDatabase attribute is added to the TestClass or TestMethod based on the use of this tessler-reset-database tag
    /// A BrowserProfile attribute is added to the TestClass or TestMethod based on the use of this tessler-browser-profile tag
    /// </summary>
    public class TesslerGeneratorProvider : MsTest2010GeneratorProvider
    {
        private const string RESETDATABASE_TAG = "tessler-reset-database-";
        private const string BROWSERPROFILE_TAG = "tessler-browser-profile-";

        private const string RESETDATABASE_ATTR = "InfoSupport.Tessler.Core.ResetDatabase";
        private const string BROWSERPROFILE_ATTR = "InfoSupport.Tessler.Core.BrowserProfile";

        /// <summary>
        /// Initializes a new instance of the <see cref="TesslerMsTest2010GeneratorProvider"/> class.
        /// </summary>
        /// <param name="codeDomHelper">The SpecFlow CodeDomHelper.</param>
        public TesslerGeneratorProvider(CodeDomHelper codeDomHelper)
            : base(codeDomHelper)
        {
        }

        /// <summary>
        /// Setup the test class
        /// </summary>
        /// <param name="generationContext"></param>
        /// <param name="featureTitle"></param>
        /// <param name="featureDescription"></param>
        public override void SetTestClass(TestClassGenerationContext generationContext, string featureTitle, string featureDescription)
        {
            base.SetTestClass(generationContext, featureTitle, featureDescription);

            SetupTesslerContext(generationContext);

            AddTestCleanupStatement(generationContext);
        }

        private void AddTestCleanupStatement(TestClassGenerationContext generationContext)
        {
            //InfoSupport.Tessler.Core.TesslerState.TestInitialize(TestContext);
            generationContext.TestCleanupMethod.Statements.Add(
                new CodeExpressionStatement(
                    new CodeMethodInvokeExpression(
                        new CodeTypeReferenceExpression(
                            "InfoSupport.Tessler.Core.TesslerState"
                        ),
                        "TestCleanup"
                    )
                )
            );
        }

        /// <summary>
        /// Setup the Tessler context
        /// </summary>
        /// <param name="generationContext">The generation context.</param>
        private static void SetupTesslerContext(TestClassGenerationContext generationContext)
        {
            AddNamespaceImports(generationContext);
            SetBaseClass(generationContext);

            // Add ResetDatabase attribute
            var reset = RetrieveResetDatabase(generationContext.Feature.Tags);
            if (reset != null)
            {
                generationContext.TestClass.CustomAttributes.Add(
                    new CodeAttributeDeclaration(RESETDATABASE_ATTR, new CodeAttributeArgument(new CodePrimitiveExpression(reset))));
            }

            // Add BrowserProfile attribute
            var profile = RetrieveBrowserProfile(generationContext.Feature.Tags);
            if (profile != null)
            {
                generationContext.TestClass.CustomAttributes.Add(
                    new CodeAttributeDeclaration(BROWSERPROFILE_ATTR, new CodeAttributeArgument(new CodePrimitiveExpression(profile))));
            }
        }

        /// <summary>
        /// Adds the namespace imports required by the generated code.
        /// </summary>
        /// <param name="generationContext">The generation context.</param>
        private static void AddNamespaceImports(TestClassGenerationContext generationContext)
        {
            generationContext.Namespace.Imports.Add(new CodeNamespaceImport("InfoSupport.Tessler.Core"));
        }

        private static void SetBaseClass(TestClassGenerationContext generationContext)
        {
            generationContext.TestClass.BaseTypes.Add(typeof(BaseFeature));
        }

        /// <summary>
        /// Setup the test initialize method
        /// </summary>
        /// <param name="generationContext"></param>
        public override void SetTestInitializeMethod(TestClassGenerationContext generationContext)
        {
            Guard.ArgumentNotNull(generationContext, "generationContext");

            base.SetTestInitializeMethod(generationContext);

            //InfoSupport.Tessler.Core.TesslerState.TestInitialize(TestContext);
            generationContext.TestInitializeMethod.Statements.Add(
                new CodeExpressionStatement(
                    new CodeMethodInvokeExpression(
                        new CodeTypeReferenceExpression(
                            "InfoSupport.Tessler.Core.TesslerState"
                            ),
                        "TestInitialize",
                        new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "TestContext"))));
        }

        /// <summary>
        /// Setup the test method
        /// </summary>
        /// <param name="generationContext"></param>
        /// <param name="testMethod"></param>
        /// <param name="scenarioTitle"></param>
        public override void SetTestMethod(TestClassGenerationContext generationContext, CodeMemberMethod testMethod, string scenarioTitle)
        {
            base.SetTestMethod(generationContext, testMethod, scenarioTitle);

            var tags = generationContext.Feature.Scenarios.Where(s => s.Title == scenarioTitle).Single().Tags;

            // Add ResetDatabase attribute
            var reset = RetrieveResetDatabase(tags);
            if (reset != null)
            {
                testMethod.CustomAttributes.Add(
                    new CodeAttributeDeclaration(RESETDATABASE_ATTR, new CodeAttributeArgument(new CodePrimitiveExpression(reset))));
            }

            // Add BrowserProfile attribute
            var profile = RetrieveBrowserProfile(tags);
            if (profile != null)
            {
                testMethod.CustomAttributes.Add(
                    new CodeAttributeDeclaration(BROWSERPROFILE_ATTR, new CodeAttributeArgument(new CodePrimitiveExpression(profile))));
            }
        }

        private static bool? RetrieveResetDatabase(Tags tags)
        {
            var tag = tags == null ? null : tags.Where(t => t.Name.StartsWith(RESETDATABASE_TAG)).FirstOrDefault();
            if (tag != null)
            {
                var postfix = tag.Name.Substring(RESETDATABASE_TAG.Length);
                if (postfix == "true")
                    return true;
                if (postfix == "false")
                    return false;
            }
            return null;
        }

        private static string RetrieveBrowserProfile(Tags tags)
        {
            var tag = tags == null ? null : tags.Where(t => t.Name.StartsWith(BROWSERPROFILE_TAG)).FirstOrDefault();
            if (tag != null)
            {
                return tag.Name.Substring(BROWSERPROFILE_TAG.Length);
            }
            return null;
        }
    }
}