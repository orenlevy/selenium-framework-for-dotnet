using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumExecuter
{
    /// <summary>
    /// This is the main class that provides the interface for
    /// the Selenium Web Driver and exposes some of the Selenium IDE
    /// commands.
    /// </summary>
    public partial class SeleniumCommandEngine
    {
        private IWebDriver _driver = null;
        private int _commandCount = 1;
        private Regex reIsJavascript = null;
        /// <summary>
        /// Creates a new command engine with a chosen web driver.
        /// </summary>
        /// <param name="driver">The instance of the web driver.</param>
        public SeleniumCommandEngine(IWebDriver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException("driver argument can't be null.");
            }
            _driver = driver;
            _driver.Manage().Timeouts().SetScriptTimeout(new TimeSpan(0, 2, 0));
            _driver.Manage().Window.Maximize();
            reIsJavascript = new Regex("^javascript{.*}$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

        }

        private string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }


        private string ReplaceTokens(string s)
        {
            Regex reex = new Regex("{(.+)}", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant);
            foreach (Match m in reex.Matches(s))
            {
                switch (m.Value.ToLower())
                {
                    case "{random_number}":
                        Random r = new Random();
                        s = s.Remove(m.Index, m.Length);
                        s = s.Insert(m.Index, string.Format("{0}", r.Next(99999)));
                        break;
                    case "{command_number}" :
                        s = s.Remove(m.Index, m.Length);
                        s = s.Insert(m.Index, string.Format("{0}", _commandCount));
                        break;
                }
            }
            return s;
        }

        /// <summary>
        /// Gets the driver instance.
        /// </summary>
        internal IWebDriver Driver
        {
            get
            {
                return _driver;
            }
        }

        /// <summary>
        /// Gets the correct By object according to the target string.
        /// </summary>
        /// <param name="target">The target element string,</param>
        /// <returns>A target's string appropriate object.</returns>
        internal By GetBy(string target)
        {
            if (target.StartsWith("//")) return By.XPath(target);
            if (target.Trim().StartsWith("css=")) return By.CssSelector(target.Substring(4));
            if (target.Trim().StartsWith("id=")) return By.Id(target.Substring(3));
            return null;
        }

        /// <summary>
        /// Indicates an element exists on the current page.
        /// </summary>
        /// <param name="target">The target's element string.</param>
        /// <returns>true if element is present.</returns>
        internal bool IsElementPresent(string target)
        {
            try
            {
                _driver.FindElement(GetBy(target));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Indicates an element is visible on the current page.
        /// </summary>
        /// <param name="target">The target's element string.</param>
        /// <returns>true if element is visible.</returns>
        internal bool IsElementVisible(string target)
        {
            try
            {
                return _driver.FindElement(GetBy(target)).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        /// <summary>
        /// Sets or get the current command index.
        /// </summary>
        internal int CurrentCommandIndex
        {
            get { return _commandCount; }
            set { _commandCount = value; }
        }

        internal void PreCommand(string command, string target, ref string value)
        {
            value = ReplaceTokens(value);
            Thread.Sleep((int)CommandEngineSettings.Speed);
            _commandCount++;
        }

        internal void PostCommand(string command, string target, string value)
        {
            
        }

        internal void ExceptionHandler(Exception ex,  string command, string target, string value)
        {
            //Assert.IsTrue(false, string.Format("Command {0}#{1} on target '{2}' with the value '{3}' has failed with {4}. ", command, _commandCount,target,value,ex.Message));
            _driver.Close();
        }
        
        public SeleniumCommandEngine ExecuteCommand(string command, string target, string value)
        {
            try
            {
                PreCommand(command, target, ref value);
                string className = string.Format("SeleniumExecuter.Commands.{0}", UppercaseFirst(command));
                var methodInfo = System.Type.GetType(className).GetMethod(command);
                methodInfo.Invoke(null, new object[] {this, target, value});
                PostCommand(command, target, value);
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex, command, target, value);
                throw;
            }
            return this;
        }


}
}
