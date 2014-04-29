using InfoSupport.Tessler.Selenium;
/****************************
 *    SpecFlow Rocks!
 ****************************/
using System;
using TechTalk.SpecFlow;
using Tessler.UITests.PageObjects;
using InfoSupport.Tessler.Core;

namespace SpecFlowTests
{
    [Binding]
    public class FormsTestSteps
    {
        [Given(@"I am on the Forms page")]
        public void GivenIAmOnThePage()
        {
            JQuery.By("a:contains('Forms')").Element().Click();
        }

        [When(@"I have entered emailaddress (.*)")]
        public void WhenIHaveEnteredEmailaddress(string address)
        {
            FormsPageObject.Create()
                .EnterEmailAddress(address)
            ;
        }

        [When(@"I have entered name (.*)")]
        public void WhenIHaveEnteredName(string name)
        {
            FormsPageObject.Create()
                .EnterName(name)
            ;
        }

        [When(@"I press submit")]
        public void WhenIPressSubmit()
        {
            FormsPageObject.Create()
                .ClickSubmit()
            ;
        }

        [Then(@"A new user has been added with emailaddress (.*) and name (.*)")]
        public void ThenANewUserHasBeenAdded(string address, string name)
        {
            FormsPageObject.Create()
                .WithSimpleFormTable(address)
                    .WithEmail(a => a.AssertEqual(address))
                    .WithName(a => a.AssertEqual(name))
            ;
        }
    }
}