using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium;

namespace Selenium.Base.Service
{
    public class AppiumUiAutomationServices : SeleniumUIAutomationService
    {
        public override void EnterTextTo(string elementKey, string text, string selectionMethod = null, string selection = null)
        {
            base.EnterTextTo(elementKey, text, selectionMethod, selection);
            var driver = ((AndroidDriver<AppiumWebElement>)GetBrowser);
            try
            {
                driver.HideKeyboard();
            }
            catch (Exception)
            {

                
            }

        }
    }
}
