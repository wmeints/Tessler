using InfoSupport.Tessler.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TesslerToysUITests
{
    [TestClass]
    public class LoginTest : TestBase
    {
        /// <summary>
        /// Dit is een voorbeeld test, waar we gebruik kunnen maken van de fluent-api.
        /// </summary>
        [TestMethod]
        public void LoginSuccessTest()
        {
            TesslerToys
                .MetH3Tekst(a => a.AssertEqual("Welcome to Tessler Toys!"))
                .KiesUpdateText()
                .MetH3Tekst(a => a.AssertEqual("Automating Fun Since 1989!"))

                .MetProduct(2)
                    .MetName(a => a.AssertEqual("Lego technic truck"))
                    .MetPrice(a => a.AssertEqual("55,00"))
                    .MetInStock(a => a.AssertEqual("23"))
                    .Then()

                .KiesPage2()

                .MetTitel(a => a.AssertEqual("Page 2"))
            ;
        }
    }
}
