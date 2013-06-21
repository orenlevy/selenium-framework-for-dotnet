using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SeleniumScriptParser.Entities;
using TestCompiler;

namespace UnitTest
{
    [TestFixture]
    public class CompilerTests
    {
        [Test]
        public void RunCompiler_Test()
        {
            List<BaseCommand> commands = new List<BaseCommand>();
            commands.Add(new BaseCommand()
            {
                Command = "open",
                Name = "open",
                Order = 1,
                Target = "http://www.google.com",
                Value = ""
            });
            commands.Add(new BaseCommand()
            {
                Command = "close",
                Name = "close",
                Order = 2,
                Target = "",
                Value = ""
            });

            Assert.IsTrue(Compiler.CreateAssembly(commands, "RunCompiler_Test"));
        }
    }
}
