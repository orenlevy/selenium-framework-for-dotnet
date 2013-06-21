using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using SeleniumExecuter.Commands;
using SeleniumScriptParser.Entities;
using SeleniumScriptParser.Importers;

namespace SeleniumExecuter
{

    public class Executer
    {

        public static void RunIDETestFile(string html, BrowserType browserType, Func<string, string, string, string, string, int> addMessage)
        {
            //create an instance of the selected web driver
            SeleniumCommandEngine engine = null;
            switch (browserType)
            {
                case BrowserType.Chrome: 
                    engine = new SeleniumCommandEngine(new ChromeDriver());           
                    break;
                case BrowserType.Firefox:
                    engine = new SeleniumCommandEngine(new FirefoxDriver());
                    break;
                case BrowserType.InternetExplorer:
                    engine = new SeleniumCommandEngine(new InternetExplorerDriver());
                    break;
            }

            //parses the commands on the test file.
            HtmlImporter importer = new HtmlImporter(html);
            ParsedHtml parsed = importer.Parse();
            int currentCommand = 0;
            try
            {
                //execute command one after another.
                foreach (var baseCommand in parsed.Commands)
                {
                    currentCommand = baseCommand.Order;
                    //send log message
                    if (addMessage != null) 
                        addMessage(baseCommand.Order.ToString(), baseCommand.Command,  baseCommand.Target, baseCommand.Value, string.Empty);
                    engine.ExecuteCommand(baseCommand.Command, baseCommand.Target, baseCommand.Value);

                }
            }
            catch (Exception ex)
            {
                if (addMessage != null)
                    addMessage(currentCommand.ToString(), string.Empty, string.Empty, string.Empty, ex.InnerException != null ? ex.InnerException.Message : string.Empty);
            }
            //closing the driver.
            engine.close();
        }

    }
}
