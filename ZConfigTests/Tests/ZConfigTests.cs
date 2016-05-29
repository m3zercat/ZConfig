using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ZConfig.Tests
{
    [TestClass]
    public class ZConfigTests
    {
        [Ignore]
        [TestMethod]
        public void TestMethod1()
        {
            Environment.SetEnvironmentVariable("CfgEnv", "environment3");
            ConfigManager.SetConfigFile(@".\config.zcf");
            Assert.AreEqual("cheese", ConfigManager.Config["Var1"]);
        }
    }
}
