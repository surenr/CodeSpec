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

                //Catching the exception when the hideKeyBoard() throws errors when the soft keyboard is not visible.
                //No other solution available at the moment, API does not have a way to check if the soft keyboard is visible still.

            }

        }
    }
}
