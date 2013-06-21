using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExecuter;

namespace TestRunnerConsole.Parameters
{
    public class ExectionParameters
    {
        /// <summary>
        /// Gets or sets the file name to be used on the test.
        /// </summary>
        public static string FileName;

        /// <summary>
        /// Gets or sets the browser type to be used on the test.
        /// </summary>
        public static BrowserType BrowserType;

        /// <summary>
        /// Gets or set the speed of the test.
        /// </summary>
        public static int Speed;
    }
}
