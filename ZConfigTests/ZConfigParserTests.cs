using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZConfigParser;
using System.Linq;

namespace ZConfigTests
{
    [TestClass]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ZConfigParserTests
    {
        [TestMethod]
        [ExpectedException(typeof (FileNotFoundException))]
        public void WhenICreateAZConfigParserWithAnIncorrectFilePathAnExceptionIsThrown()
        {
            var zcf = new ZConfigFileParser(@"C:\A\Place\That\Doesnt\Exist\config.zcf");
        }

        [TestMethod]
        public void WhenICreateAZConfigParserWithAFileThatCanBeReadNoExceptionIsThrown()
        {
            File.WriteAllText(@".\empty.zcf", "");
            var zcf = new ZConfigFileParser(@".\empty.zcf");
        }

        [TestMethod]
        public void WhenIReadAConfigFileWithOneSectionOnlyOneSectionIsReturnedWithTheRightName()
        {
            const String sectionName = "Flubber";
            File.WriteAllText(@".\singleSection.zcf", String.Format("{0}{0}[{1}]{0}A = b{0}", Environment.NewLine, sectionName));
            var zcf = new ZConfigFileParser(@".\singleSection.zcf");
            var conf = zcf.Read();
            Assert.AreEqual(1, conf.Count, "Unexpected number of config sections");
            Assert.AreEqual(sectionName, conf.First().Name, "The config section name does not match the expectation");
        }

        [TestMethod]
        public void WhenIReadAConfigFileWithTwoSectionsBothAreReturned()
        {
            const String sectionA = "AAA";
            const String sectionB = "BBA";

            File.WriteAllText(@".\doubleSection.zcf", String.Format("{0}{0}[{1}]{0}A1 = b{0}A2 = c{0}[{2}]{0}B1 = a", Environment.NewLine, sectionA, sectionB));
            var zcf = new ZConfigFileParser(@".\doubleSection.zcf");
            var conf = zcf.Read();
            Assert.AreEqual(2, conf.Count, "Unexpected number of config sections");
            Assert.IsTrue(conf.Any(x => x.Name == sectionA), "The config section " + sectionA + " is missing");
            Assert.IsTrue(conf.Any(x => x.Name == sectionB), "The config section " + sectionB + " is missing");
        }

        [TestMethod]
        public void WhenIReadAConfigFileWithOneSectionInheritingAnotherTheStructureIsAsExpected()
        {
            const String sectionA = "AAA";
            const String sectionB = "BBA";

            File.WriteAllText(@".\doubleSectionWithInheritance.zcf", String.Format("{0}{0}[{1}:{2}]{0}A1 = b{0}A2 = c{0}[{2}]{0}B1 = a", Environment.NewLine, sectionA, sectionB));
            var zcf = new ZConfigFileParser(@".\doubleSectionWithInheritance.zcf");
            var conf = zcf.Read();
            Assert.AreEqual(2, conf.Count, "Unexpected number of config sections");
            Assert.IsTrue(conf.Any(x => x.Name == sectionA && x.InheritsFromSection == sectionB), "The config section " + sectionA + " is missing or does not inherit from "+sectionB);
            Assert.IsTrue(conf.Any(x => x.Name == sectionB && x.InheritsFromSection == null), "The config section " + sectionB + " is missing or the inherit value is not null");
        }
    }
}
