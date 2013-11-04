using System;
using InfoSupport.Tessler.Core;
using InfoSupport.Tessler.Selenium;

namespace PageObjects
{
    /// <summary>
    /// Dit page object stelt de home pagina voor, met een paar knoppen en een tabel.
    /// </summary>
    public class HomePageObject : TesslerObject<HomePageObject>
    {
        /// <summary>
        /// Deze MetXxx methodes worden gebruikt om te asserten en eventueel waardes van de pagina op te slaan in variabelen
        /// </summary>
        public HomePageObject MetH3Tekst(Action<string> action)
        {
            action(JQuery.By("h3").Element().Text);

            return ResolveSelf();
        }

        /// <summary>
        /// Met KiesXxx worden bepaalde knoppen ingedrukt en items in select lists geselecteerd
        /// </summary>
        public HomePageObject KiesUpdateText()
        {
            JQuery.By("#update-text").Element().Click();

            return ResolveSelf();
        }

        public PageTwoPageObject KiesPage2()
        {
            JQuery.By("li:contains('Page 2') > a").Element().Click();

            return Resolve<PageTwoPageObject>();
        }

        /// <summary>
        /// Deze methode geeft een scope object terug voor de tabel
        /// </summary>
        public ProductScopeObject MetProduct(int id)
        {
            var pageObject = Resolve<ProductScopeObject>();

            pageObject.Id = id;

            return pageObject;
        }

        /// <summary>
        /// Scope object, stelt een regel in de tabel voor
        /// </summary>
        public class ProductScopeObject : ScopeObject<ProductScopeObject, HomePageObject>
        {
            /// <summary>
            /// Met behulp van het Id kunnen we een specifieke regel selecteren
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Een groot deel van de selector kunnen we hergebruiken tussen verschillende onderdelen in de tabel
            /// </summary>
            private JQuery Selector
            {
                get { return JQuery.By("table#products tr td:equals('{0}')", Id.ToString()); }
            }

            public ProductScopeObject MetId(Action<string> action)
            {
                action(Selector.Sibling(1).Element().Text);

                return ResolveSelf();
            }

            public ProductScopeObject MetName(Action<string> action)
            {
                action(Selector.Sibling(2).Element().Text);

                return ResolveSelf();
            }

            public ProductScopeObject MetPrice(Action<string> action)
            {
                action(Selector.Sibling(3).Element().Text);

                return ResolveSelf();
            }

            public ProductScopeObject MetInStock(Action<string> action)
            {
                action(Selector.Sibling(4).Element().Text);

                return ResolveSelf();
            }
        }
    }
}
