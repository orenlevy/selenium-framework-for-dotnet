using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumExecuter
{
    public static class CommandEngineSettings
    {
        /// <summary>
        /// controls the wait for element present or not present.
        /// </summary>
        public static int WaitForElementTimeOut = 30;

        /// <summary>
        /// The time the click and wait command waits after a click
        /// </summary>
        public static int ClickAndWaitTimeOut = 30;

        /// <summary>
        /// Number of milliseconds to wait.
        /// </summary>
        public static double Speed = 500;
    }
}
