using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SeleniumExecuter;

namespace UnitTest
{
    [TestFixture]
    public class ExecuterTests
    {
        [Test]
        public void RunSimpleScript()
        {
            try
            {
                Executer.RunIDETestFile(ExecuterResouces.TestScript, BrowserType.Firefox, null);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, string.Format("RunSimpleScript failed - {0},{1}", ex.Message, ex.InnerException != null ? ex.InnerException.Message : string.Empty));
            }
            Assert.IsTrue(true);
        }

    }
}
