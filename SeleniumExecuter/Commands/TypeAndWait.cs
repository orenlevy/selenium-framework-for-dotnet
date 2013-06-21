using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using SeleniumExecuter.WebDriverExtenstions;

namespace SeleniumExecuter.Commands
{
    public static class TypeAndWait
    {
        /// <summary>
        /// Simulate the typeAndWait command.
        /// </summary>
        /// <param name="engine">Selenium Engine instance.</param>
        /// <param name="target">Target element string.</param>
        /// <param name="value">Value string.</param>
        /// <returns>The instance of the engine.</returns>
        public static SeleniumCommandEngine typeAndWait(this SeleniumCommandEngine engine, string target, string value)
        {
            IWebElement element = engine.Driver.FindElement(engine.GetBy(target));
            if (element.TagName.ToLower() == "input")
            {
                element.SetAttribute("value", value);
            }
            element.SendKeys(Keys.Enter);
            Thread.Sleep(CommandEngineSettings.ClickAndWaitTimeOut);
            return engine;
        }
    }
}
