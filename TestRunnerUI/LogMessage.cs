using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestRunnerUI
{
    /// <summary>
    /// This is a container class for log message passed asynchronously 
    /// to the main form to be logged on screen.
    /// </summary>
    class LogMessage
    {
        /// <summary>
        /// Gets or sets the line number
        /// </summary>
        public string Line { get; set; }

        /// <summary>
        /// Gets or sets the command executed.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// Gets or sets the target element of the command.
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets the value of the command.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string Error { get; set; }
    }
}
