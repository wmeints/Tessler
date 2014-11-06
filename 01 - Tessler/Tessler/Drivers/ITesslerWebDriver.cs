using System.Collections.Generic;
using InfoSupport.Tessler.Selenium;
using OpenQA.Selenium;

namespace InfoSupport.Tessler.Drivers
{
    public interface ITesslerWebDriver
    {
        #region Driver specific

        /// <summary>
        /// Returns whether the driver is still active
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Closes the browser and the webdriver instance
        /// </summary>
        void Close();

        /// <summary>
        /// Gets the source of the current page
        /// </summary>
        string PageSource { get; }

        /// <summary>
        /// Gets the title of the current page
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Sets or gets the page url
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// Returns the Selenium driver
        /// </summary>
        IWebDriver GetDriver();

        /// <summary>
        /// Creates a screenshot
        /// </summary>
        Screenshot GetScreenshot();

        /// <summary>
        /// Instructs the driver to change its settings
        /// </summary>
        /// <returns></returns>
        IOptions Manage();

        /// <summary>
        /// Instructs the driver to navigate the browser to another location
        /// </summary>
        /// <returns></returns>
        INavigation Navigate();

        /// <summary>
        /// Closes any windows on top of the actual browser window
        /// </summary>
        void ClosePopups();

        /// <summary>
        /// Pauses execution for a short period of time, using the constant from the selenium settings
        /// </summary>
        void Wait();

        #endregion

        #region Javascript

        bool IsJQueryLoaded { get; }

        /// <summary>
        /// Checks if jQuery has been loaded and loads it if no version is available
        /// </summary>
        void LoadJQuery();

        /// <summary>
        /// Executes the specified piece of javascript and returns any result it might give
        /// </summary>
        /// <param name="javascript">The javascript to run</param>
        /// <returns>The result of the script, which can be an IWebElement</returns>
        object Js(string javascript);

        /// <summary>
        /// Waits for any pending ajax connections to close before continueing
        /// </summary>
        void WaitForAjax();

        #endregion

        #region Retrieval

        /// <summary>
        /// Searches for elements based on the specified jQuery selector
        /// </summary>
        /// <param name="by">The jQuery selector to use</param>
        /// <returns>The resulting elements</returns>
        IEnumerable<IJQueryElement> FindElements(JQuery by);

        /// <summary>
        /// Checks that the specified element is not visible
        /// </summary>
        /// <param name="by">The jQuery selector to use</param>
        /// <returns>Whether the element is indeed invisible</returns>
        bool IsVisible(JQuery by);

        /// <summary>
        /// Tries to find elements, retrying until a timeout occurs or the elements are found
        /// </summary>
        /// <param name="by">The jQuery selector to use</param>
        /// <returns>List of JQueryElements</returns>
        IEnumerable<IJQueryElement> WaitFor(JQuery by);

        #endregion
    }
}