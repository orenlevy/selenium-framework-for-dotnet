using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumExecuter.Commands
{
    public static class Open
    {
        /// <summary>
        /// Simulate the open command.
        /// </summary>
        /// <param name="engine">Selenium Engine instance.</param>
        /// <param name="target">Target element string.</param>
        /// <param name="value">Value string.</param>
        /// <returns>The instance of the engine.</returns>
        public static SeleniumCommandEngine open(this SeleniumCommandEngine engine, string target, string value)
        {
            engine.Driver.Url = target;
            return engine;
        }
    }
}
