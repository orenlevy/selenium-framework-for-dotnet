using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumExecuter.Commands
{
    public static class Close
    {
        /// <summary>
        /// Simulate the close command.
        /// </summary>
        /// <param name="engine">Selenium Engine instance.</param>
        /// <param name="target">Target element string.</param>
        /// <param name="value">Value string.</param>
        /// <returns>The instance of the engine.</returns>
        public static SeleniumCommandEngine close(this SeleniumCommandEngine engine)
        {
            engine.Driver.Close();
            return engine;
        }
    }
}
