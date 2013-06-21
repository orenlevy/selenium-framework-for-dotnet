using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using SeleniumExecuter;
using SeleniumExecuter.Commands;

namespace UnitTest
{
    [TestFixture]
    public class EngineTests
    {
        private SeleniumCommandEngine engine;

        [Test]
        public void OpenCommand_Test()
        {
            try
            {
                engine = new SeleniumCommandEngine(new FirefoxDriver());
                engine
                    .open("http://localhost:3000", null)
                    .close();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, string.Format("command failed - {0}", ex.Message));
                engine.close();
            }
        }

        [Test]
        public void ClickCommand_Test()
        {
            try
            {
                engine = new SeleniumCommandEngine(new FirefoxDriver());
                engine
                    .open("http://www.google.com", null)
                    .click("//input[@name='btnK']", null)
                    .close();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, string.Format("command failed - {0}", ex.Message));
                engine.close();
            }
        }

        [Test]
        public void WaitForElementPresentCommand_NoElementFound_Test()
        {
            try
            {
                engine = new SeleniumCommandEngine(new FirefoxDriver());
                engine
                    .open("http://www.google.com", null)
                    .waitForElementPresent("//input[@name='btnK222']", null)
                    .click("//input[@name='btnK']", null)
                    .close();
            }
            catch
            {
                engine.close();
                Assert.IsTrue(true);
                return;
            }
            engine.close();
            Assert.IsTrue(false, string.Format("command failed - waitForElementPresent returned without throwing an exception."));
        }


        [Test]
        public void WaitForElementNotPresentCommand_Test()
        {
            try
            {
                engine = new SeleniumCommandEngine(new FirefoxDriver());
                engine
                    .open("http://localhost:3000/Commands/WaitForElementNotPresent", null)
                    .waitForElementNotPresent("//input", null)
                    .close();
            }
            catch (Exception ex)
            {
                engine.close();
                Assert.IsTrue(false, string.Format("command failed - {0}", ex.Message));
            }
            Assert.IsTrue(true);
        }

        [Test]
        public void Type_Test()
        {
            try
            {
                engine = new SeleniumCommandEngine(new FirefoxDriver());
                engine
                    .open("http://localhost:3000/Commands/TypeCommand", null)
                    .type("id=textarea", "This is a test text")
                    .close();
            }
            catch (Exception ex)
            {
                engine.close();
                Assert.IsTrue(false, string.Format("command failed - {0}", ex.Message));
            }
            Assert.IsTrue(true);   
        }

        [Test]
        public void SendKeys_Test()
        {
            try
            {
                engine = new SeleniumCommandEngine(new FirefoxDriver());
                engine
                    .open("http://localhost:3000/Commands/SendKeys", null)
                    .sendKeys("id=text", "This is a test text")
                    .close();
            }
            catch (Exception ex)
            {
                engine.close();
                Assert.IsTrue(false, string.Format("command failed - {0}", ex.Message));
            }
            Assert.IsTrue(true);  
        }

        [Test]
        public void ClickAndWait_Test()
        {
            try
            {
                engine = new SeleniumCommandEngine(new FirefoxDriver());
                engine
                    .open("http://localhost:3000/Commands/ClickAndWait", null)
                    .clickAndWait("id=text", null)
                    .close();
            }
            catch (Exception ex)
            {
                engine.close();
                Assert.IsTrue(false, string.Format("command failed - {0}", ex.Message));
            }
            Assert.IsTrue(true);
        }

        [Test]
        public void VerifyValue_Test()
        {
            try
            {
                engine = new SeleniumCommandEngine(new FirefoxDriver());
                engine
                    .open("http://localhost:3000/Commands/VerifyValue", null)
                    .verifyValue("id=text", "Test123")
                    .verifyValue("id=textarea", "Test123")
                    .close();
            }
            catch (Exception ex)
            {
                engine.close();
                Assert.IsTrue(false, string.Format("command failed - {0}", ex.Message));
            }
            Assert.IsTrue(true);
        }

        [Test]
        public void VerifyText_Test()
        {
            try
            {
                engine = new SeleniumCommandEngine(new FirefoxDriver());
                engine
                    .open("http://localhost:3000/Commands/VerifyText", null)
                    .verifyText("id=testcontent", "Testing 123")
                    .close();
            }
            catch (Exception ex)
            {
                engine.close();
                Assert.IsTrue(false, string.Format("command failed - {0}", ex.Message));
            }
            Assert.IsTrue(true);
        }
    }
}
