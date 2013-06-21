using System.Net;
using HtmlAgilityPack;
using SeleniumScriptParser.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumScriptParser.Importers
{
    /// <summary>
    /// The class takes an Selenium HTML test formatted string and extracts the command
    /// it contains.
    /// </summary>
    public class HtmlImporter
    {
        private string _html;
        private HtmlDocument _htmlDoc;
        private string _scriptName;

        /// <summary>
        /// Creates a new HtmlImporter.
        /// </summary>
        /// <param name="html">An HTML string.</param>
        public HtmlImporter(string html)
        {
            if (html == null)
            {
                throw new ArgumentNullException("html can't be null");
            }
            _html = html;
            _htmlDoc = new HtmlDocument();
            _htmlDoc.LoadHtml(_html);
        }


        /// <summary>
        /// Gets a list of Selenium commands embedded in the HTML.
        /// </summary>
        /// <returns></returns>
        private List<BaseCommand> ParseCommands()
        {
            List<BaseCommand> parsedCommands = new List<BaseCommand>();

            //finds the name of the script.
            foreach (HtmlNode item in _htmlDoc.DocumentNode.SelectNodes("//thead/tr/td"))
            {
                _scriptName = item.InnerText;    
            }

            //parse the commands on the HTML.
            int order = 1;
            foreach (HtmlNode node in _htmlDoc.DocumentNode.SelectNodes("//tbody/tr[position()>0]"))
            {
                BaseCommand newCommand = new BaseCommand()
                {
                    Order = order++,
                    Name = _scriptName
                };

                int tdIndex = 0;
                foreach (HtmlNode td in node.Elements("td"))
                {
                    switch (tdIndex++)
                    {
                        case 0: newCommand.Command = td.InnerText.Trim();
                            break;
                        case 1:
                            newCommand.Target = WebUtility.HtmlDecode(td.InnerText.Trim());
                            break;
                        case 2:
                            newCommand.Value = WebUtility.HtmlDecode(td.InnerText.Trim());
                            break;
                        default:
                            break;
                    }
                }
                parsedCommands.Add(newCommand);

            }
            return parsedCommands;
        }

        /// <summary>
        /// Extract the selenium base url.
        /// </summary>
        /// <returns></returns>
        private string ParseSeleniumBase()
        {
            foreach (HtmlNode item in _htmlDoc.DocumentNode.SelectNodes("//head/link"))
            {
                string attributeVal = item.GetAttributeValue("rel", null);
                if (!string.IsNullOrEmpty(attributeVal))
                {
                    if (attributeVal.Equals("selenium.base"))
                    {
                        return item.GetAttributeValue("href", null);
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Parse the test html.
        /// </summary>
        /// <returns>An object wrapping the commands and Selenium Base Url.</returns>
        public ParsedHtml Parse()
        {
            ParsedHtml result = new ParsedHtml();
            result.Commands = ParseCommands();
            result.SeleniumBase = ParseSeleniumBase();

            if (result.Commands.Count() > 0 
                && result.Commands[0].Command.Equals("open") 
                && result.Commands[0].Target.Equals("/"))
            {
                result.Commands[0].Target = result.SeleniumBase;
                if (!result.SeleniumBase.EndsWith("/"))
                {
                    result.Commands[0].Target += "/";
                }
            }

            return result;
        }
    }
}
