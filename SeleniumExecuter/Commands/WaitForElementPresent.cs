using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SeleniumExecuter.Commands
{
    public static class WaitForElementPresent
    {
        /// <summary>
        /// Simulate the waitForElementPresent command.
        /// </summary>
        /// <param name="engine">Selenium Engine instance.</param>
        /// <param name="target">Target element string.</param>
        /// <param name="value">Value string.</param>
        /// <returns>The instance of the engine.</returns>
        public static SeleniumCommandEngine waitForElementPresent(this SeleniumCommandEngine engine, string target,
                                                                  string value)
        {
            int second = 0;
            for (second = 0; second < CommandEngineSettings.WaitForElementTimeOut; second++)
            {
                Thread.Sleep(1000);
                try
                {
                    if (engine.IsElementPresent(target))
                    {
                        return engine;
                    }
                }
                catch (Exception)
                {
                }
            }
            throw new TimeoutException("Element was not found");
        }
    }
}
