using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRunnerConsole.Parameters;
using SeleniumExecuter;

namespace TestRunnerConsole
{
    /// <summary>
    /// A console application for running the Selenium IDE Test files.
    /// </summary>
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (CommandLineParser.Parse(args))
            {
                CommandEngineSettings.Speed = ExectionParameters.Speed;
                Executer.RunIDETestFile(File.ReadAllText(ExectionParameters.FileName), ExectionParameters.BrowserType,
                                   LogMessage);

            }
#if DEBUG
            Console.ReadKey();
#endif
        }

        /// <summary>
        /// Log the message to the screen.
        /// </summary>
        /// <param name="line">The number of line that is being executed.</param>
        /// <param name="command">Command that is executed.</param>
        /// <param name="target">Target element the command is executed on.</param>
        /// <param name="value">The value used from the command.</param>
        /// <param name="error">Error message if an error has occurred.</param>
        private static int LogMessage(string line, string command, string target, string value, string error)
        {
            Console.WriteLine(string.Format("#{0} - {1} - target :  {2} - value : {3}", line, command, target, value));
            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine(string.Format("#{0} error : {1}", line, error));
            }
            else
            {
                Console.WriteLine(string.Format("#{0} - {1} - target :  {2} - value : {3}", line, command, target, value));    
            }
            return 0;
        }

    }
}
