using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZConfig.Helpers;

namespace ZConfig.Interpretation.Tests
{
    [TestClass]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class InterpretationTests
    {
        [TestMethod]
        [Ignore]
        [ExpectedException(typeof(FileNotFoundException))]
        public void T001_WhenIDoSomething()
        {
            IRawConfigSection rawSection = (new RawConfigSectionBuilder())
                .WithName("SampleSection")
                .WithConfigLine("Var1", "a")
                .WithConfigLine("Var2", "b")
                .Build();
            IRawConfiguration rawConfig = (new RawConfigurationBuilder())
                .WithSection(rawSection)
                .Build();
            Assert.Inconclusive("No idea what i did!");
        }
    }
}
