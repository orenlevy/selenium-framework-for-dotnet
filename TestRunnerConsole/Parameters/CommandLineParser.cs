using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExecuter;

namespace TestRunnerConsole.Parameters
{
    /// <summary>
    /// Parses the command line parameters and sets the
    /// the run ExectionParameter accordingly.
    /// </summary>
    public class CommandLineParser
    {
        public static bool Parse(string[] args)
        {
            if (args.Length == 0)
            {
                HelpMessage();
                return false;
            }
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                        //display a help message
                    case "--help": HelpMessage();
                        return false;
                        //set the file name to be user for the test.
                    case "-f":
                        if (args.Length - 1 >= i + 1)
                        {
                            ExectionParameters.FileName = args[++i];
                        }
                        else
                        {
                            Console.WriteLine("Error - Missing file name");
                            return false;
                        }
                        break;
                        //sets the speed of the test.
                    case "-s":
                        if (args.Length - 1 >= i + 1)
                        {
                            int speed = 0;
                            if (int.TryParse(args[++i], out speed))
                                ExectionParameters.Speed = speed;
                            else
                            {
                                Console.WriteLine("Error - Incorrect speed value.");
                                return false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error - Missing speed.");
                            return false;
                        }
                        break;
                        //sets the browser type for the test
                    case "-b":
                        if (args.Length - 1 >= i + 1)
                        {
                            switch (args[++i].ToLower())
                            {
                                case "ie": 
                                    ExectionParameters.BrowserType = BrowserType.InternetExplorer;
                                    break;
                                case "firefox": 
                                    ExectionParameters.BrowserType = BrowserType.Firefox;
                                    break;
                                case "chrome": 
                                    ExectionParameters.BrowserType = BrowserType.Chrome;
                                    break;
                                default:
                                    Console.WriteLine("Error - Browser type not supported.");
                                    return false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error - Missing browser type.");
                            return false;
                        }
                        break;
                }
            }
            return true;

        }

        /// <summary>
        /// Write a help message to the console.
        /// </summary>
        public static void HelpMessage()
        {
            Console.WriteLine("Test Runner- Command line application for running Selenium HTML tests.");
            Console.WriteLine();
            Console.WriteLine("--help - This help message.");
            Console.WriteLine("-f <FileName> - HTML test file.");
            Console.WriteLine("-s <Milliseconds> - Delay in milliseconds between each command");
            Console.WriteLine("-b <IE|Firefox|Chrome> - Select the browser you want to run your script.");
            Console.WriteLine();

        }
    }
}
