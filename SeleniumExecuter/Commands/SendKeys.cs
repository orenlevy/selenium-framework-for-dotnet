using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SeleniumExecuter.WebDriverExtenstions;

namespace SeleniumExecuter.Commands

{
    public static class SendKeys
    {
        /// <summary>
        /// Simulate the sendKeys command.
        /// </summary>
        /// <param name="engine">Selenium Engine instance.</param>
        /// <param name="target">Target element string.</param>
        /// <param name="value">Value string.</param>
        /// <returns>The instance of the engine.</returns>
        public static SeleniumCommandEngine sendKeys(this SeleniumCommandEngine engine, string target, string value)
        {
            IWebElement element = engine.Driver.FindElement(engine.GetBy(target));
            for (int i = 0; i < value.Length; i += 8)
            {
                int length = 8;
                if (i + length > value.Length)
                {
                    length =  value.Length - i;
                }
                element.SendKeys(value.Substring(i,length));
            }
           
            return engine;
        }
    }
}
