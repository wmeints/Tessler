﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSupport.Tessler.Drivers
{
    public interface IWebDriverFactory
    {
        IWebDriver BuildWebDriver();
    }
}
