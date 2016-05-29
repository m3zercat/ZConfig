using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Assert.Inconclusive("No idea what i did!");
        }
    }
}
