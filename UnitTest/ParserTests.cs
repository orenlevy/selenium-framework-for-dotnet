using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SeleniumScriptParser.Entities;
using SeleniumScriptParser.Importers;

namespace UnitTest
{
    [TestFixture]
    public class ParserTests
    {

        /// <summary>
        /// The test load a 3 commands html file parses the html
        /// and checks the commands parsed are valid.
        /// The parsed HTML has command that are distorted.
        /// All commands on the HTML should validate for the test to succeed.
        /// </summary>
        [Test]
        public void Parse_Html_To_Commands()
        {
            HtmlImporter htmlImporter = new HtmlImporter(ParserResources.Html_For_Load_Html_File);
            List<BaseCommand> parsedCommands = htmlImporter.Parse().Commands;
            Assert.IsTrue(parsedCommands.Count == 3, "Some command were not parsed");
            BaseCommand command1 = new BaseCommand()
                {
                    Target = "//div",
                    Command = "Command1",
                    Order =  1,
                    Name = "TestName",
                    Value = "Value1"
                };
            Assert.AreEqual(command1, parsedCommands[0], "First command was not parsed");
            BaseCommand command2 = new BaseCommand()
            {
                Target = "css=a.class",
                Command = "Command2",
                Order = 2,
                Name = "TestName",
                Value = "Value2"
            };
            Assert.AreEqual(command2, parsedCommands[1], "Second command was not parsed");
            BaseCommand command3 = new BaseCommand()
            {
                Target = "id=testId",
                Command = "Command3",
                Order = 3,
                Name = "TestName",
                Value = "Value3"
            };
            Assert.AreEqual(command3, parsedCommands[2], "Third command was not parsed");
        }

        [Test]
        public void Add_Base_URL_To_First_Command()
        {
            HtmlImporter htmlImporter = new HtmlImporter(ParserResources.Base_Url_Test);
            ParsedHtml parsed = htmlImporter.Parse();
            Assert.AreEqual("https://www.facebook.com/", parsed.Commands[0].Target,  string.Format("selenium.base was not parsed correctly - {0}", parsed.SeleniumBase));
        }
    }
}
