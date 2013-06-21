using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumExecuter.Commands
{
    public static class ClickAndWait
    {
        /// <summary>
        /// Simulate the click and wait command.
        /// </summary>
        /// <param name="engine">Selenium Engine instance.</param>
        /// <param name="target">Target element string.</param>
        /// <param name="value">Value string.</param>
        /// <returns>The instance of the engine.</returns>
        public static SeleniumCommandEngine clickAndWait(this SeleniumCommandEngine engine, string target, string value)
        {
            engine.click(target, value);
            Thread.Sleep(CommandEngineSettings.ClickAndWaitTimeOut * 1000);
            return engine;
        }
    }
}
