using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumExecuter.Commands
{
    public static class VerifyText
    {
        /// <summary>
        /// Test the inner text of an HTML element.
        /// </summary>
        /// <param name="engine">Selenium Engine instance.</param>
        /// <param name="target">Target element string.</param>
        /// <param name="value">Value string.</param>
        /// <returns>The instance of the engine.</returns>
        public static SeleniumCommandEngine verifyText(this SeleniumCommandEngine engine, string target, string value)
        {
            IWebElement element = engine.Driver.FindElement(engine.GetBy(target));
            Assert.IsTrue(element.Text.Trim() == value);
            return engine;
        }
    }
}
