using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeleniumExecuter;

namespace TestRunnerUI
{
    /// <summary>
    /// The class contains a file name collected from the UI.
    /// </summary>
    class UiMessageContainer
    {
        /// <summary>
        /// Gets or sets the file name selected by the user.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the browser type selected.
        /// </summary>
        public BrowserType BrowserType { get; set; }

        /// <summary>
        /// Gets or sets the speed in which to run the test.
        /// The higher the number the lower the test is.
        /// </summary>
        public double Speed { get; set; }
    }
}
