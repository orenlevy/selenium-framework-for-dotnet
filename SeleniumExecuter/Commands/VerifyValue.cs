using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumExecuter.Commands
{
    public static class VerifyValue
    {
        /// <summary>
        /// Test the value of an input or a text area.
        /// </summary>
        /// <param name="engine">Selenium Engine instance.</param>
        /// <param name="target">Target element string.</param>
        /// <param name="value">Value string.</param>
        /// <returns>The instance of the engine.</returns>
        public static SeleniumCommandEngine verifyValue(this SeleniumCommandEngine engine, string target, string value)
        {
            IWebElement element = engine.Driver.FindElement(engine.GetBy(target));
            string val = string.Empty;
            switch (element.TagName.ToLower())
            {
                case "input" :
                    val = element.GetAttribute("value");
                    break;
                case "textarea":
                    val = element.Text;
                    break;
            }
            Assert.IsTrue(val == value);
            return engine;
        }

        
    }
}
