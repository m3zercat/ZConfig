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
        public void T001_WhenICreateAZConfigParserWithAnIncorrectFilePathAnExceptionIsThrown()
        {
            ZConfigFileParser zcf = new ZConfigFileParser(@".\t001.zcf");
        }

        [TestMethod]
        public void T002_WhenICreateAZConfigParserWithAFileThatCanBeReadNoExceptionIsThrown()
        {
            File.WriteAllText(@".\t002.zcf", "");
            ZConfigFileParser zcf = new ZConfigFileParser(@".\t002.zcf");
        }

        [TestMethod]
        public void T003_WhenIReadAConfigFileWithOneSectionOnlyOneSectionIsReturnedWithTheRightName()
        {
            const String sectionName = "Flubber";
            File.WriteAllText(@".\t003.zcf", String.Format("{0}{0}[{1}]{0}A = b{0}", Environment.NewLine, sectionName));
            ZConfigFileParser zcf = new ZConfigFileParser(@".\t003.zcf");
            Configuration conf = zcf.Read();
            Assert.AreEqual(1, conf.Count, "Unexpected number of config sections");
            Assert.AreEqual(sectionName, conf.First().Key, "The config section name does not match the expectation");
        }

        [TestMethod]
        public void T004_WhenIReadAConfigFileWithTwoSectionsBothAreReturned()
        {
            const String sectionA = "AAA";
            const String sectionB = "BBA";

            File.WriteAllText(@".\t004.zcf", String.Format("{0}{0}[{1}]{0}A1 = b{0}A2 = c{0}[{2}]{0}B1 = a", Environment.NewLine, sectionA, sectionB));
            ZConfigFileParser zcf = new ZConfigFileParser(@".\t004.zcf");
            Configuration conf = zcf.Read();
            Assert.AreEqual(2, conf.Count, "Unexpected number of config sections");
            Assert.IsTrue(conf.Any(x => x.Key == sectionA), "The config section " + sectionA + " is missing");
            Assert.IsTrue(conf.Any(x => x.Key == sectionB), "The config section " + sectionB + " is missing");
        }

        [TestMethod]
        public void T005_WhenIReadAConfigFileWithOneSectionInheritingAnotherTheStructureIsAsExpected()
        {
            const String sectionA = "AAA";
            const String sectionB = "BBA";

            File.WriteAllText(@".\t005.zcf", String.Format("{0}{0}[{1}:{2}]{0}A1 = b{0}A2 = c{0}[{2}]{0}B1 = a", Environment.NewLine, sectionA, sectionB));
            ZConfigFileParser zcf = new ZConfigFileParser(@".\t005.zcf");
            Configuration conf = zcf.Read();
            Assert.AreEqual(2, conf.Count, "Unexpected number of config sections");
            Assert.IsTrue(conf.Any(x => x.Key == sectionA && x.Value.InheritsFromSection == sectionB), "The config section " + sectionA + " is missing or does not inherit from "+sectionB);
            Assert.IsTrue(conf.Any(x => x.Key == sectionB && x.Value.InheritsFromSection == null), "The config section " + sectionB + " is missing or the inherit value is not null");
        }

        [TestMethod]
        public void T006_WhenIReadAConfigFileOnlyTheCorrectLinesArePresentInTheCorrectSections()
        {
            File.WriteAllText(@".\t006.zcf", String.Format("{0}{0}[{1}:{2}]{0}A1 = b{0}{0}A2 = c{0}[{2}]{0}B1 = a", Environment.NewLine, "SectionA", "SectionB"));
            ZConfigFileParser zcf = new ZConfigFileParser(@".\t006.zcf");
            Configuration conf = zcf.Read();
            Assert.AreEqual(2, conf.Count, "Unexpected number of config sections");
            ConfigSection sectionA = conf["SectionA"];
            Assert.AreEqual(2, sectionA.Lines.Count, "Unexpected number of lines in Section A");
            Assert.IsTrue(sectionA.Lines.ContainsKey("A2"), "Section A does not contain variable 'A2'");
            Assert.AreEqual("c", sectionA.Lines["A2"], "Section A variable 'A2' did not contain value 'c'");
            Assert.IsTrue(sectionA.Lines.ContainsKey("A1"), "Section A does not contain variable 'A1'");
            Assert.AreEqual("b", sectionA.Lines["A1"], "Section A variable 'A1' did not contain value 'b'");
            ConfigSection sectionB = conf["SectionB"];
            Assert.AreEqual(1, sectionB.Lines.Count, "Unexpected number of lines in Section B");
            Assert.IsTrue(sectionB.Lines.ContainsKey("B1"), "Section B does not contain variable 'B1'");
            Assert.AreEqual("a", sectionB.Lines["B1"], "Section B variable 'B1' did not contain value 'a'");
        }

        [TestMethod]
        public void T007_WhenIReadAConfigFileContainingCommentsOnlyTheCorrectLinesArePresent()
        {
            File.WriteAllText(@".\t007.zcf", String.Format("{0}#Comment 1{0}{0}{0}[{1}]{0}A1 = b{0}#Comment=2{0}A2 = c{0}{0}#[Comment] 3{0}#[blah d blah]{0}#Comment 4{0}{0}#B1 = a{0}#Comment 5{0}", Environment.NewLine, "SectionA"));
            ZConfigFileParser zcf = new ZConfigFileParser(@".\t007.zcf");
            Configuration conf = zcf.Read();
            Assert.AreEqual(1, conf.Count, "Unexpected number of config sections");
            ConfigSection sectionA = conf["SectionA"];
            Assert.AreEqual(2, sectionA.Lines.Count, "Unexpected number of lines in Section A");
            Assert.IsTrue(sectionA.Lines.ContainsKey("A2"), "Section A does not contain variable 'A2'");
            Assert.AreEqual("c", sectionA.Lines["A2"], "Section A variable 'A2' did not contain value 'c'");
            Assert.IsTrue(sectionA.Lines.ContainsKey("A1"), "Section A does not contain variable 'A1'");
            Assert.AreEqual("b", sectionA.Lines["A1"], "Section A variable 'A1' did not contain value 'b'");
        }

        [TestMethod]
        public void T008_WhenIReadAConfigFileContainingIncompleteLinesOnlyTheCorrectLinesArePresent()
        {
            File.WriteAllText(@".\t008.zcf", String.Format("{0}[cards{0}{0}[{1}]{0}A1 = b{0}{0}=2{0}2={0}A2 = c{0}{0}", Environment.NewLine, "SectionA"));
            ZConfigFileParser zcf = new ZConfigFileParser(@".\t008.zcf");
            Configuration conf = zcf.Read();
            Assert.AreEqual(1, conf.Count, "Unexpected number of config sections");
            ConfigSection sectionA = conf["SectionA"];
            Assert.AreEqual(2, sectionA.Lines.Count, "Unexpected number of lines in Section A");
            Assert.IsTrue(sectionA.Lines.ContainsKey("A2"), "Section A does not contain variable 'A2'");
            Assert.AreEqual("c", sectionA.Lines["A2"], "Section A variable 'A2' did not contain value 'c'");
            Assert.IsTrue(sectionA.Lines.ContainsKey("A1"), "Section A does not contain variable 'A1'");
            Assert.AreEqual("b", sectionA.Lines["A1"], "Section A variable 'A1' did not contain value 'b'");
        }

        [TestMethod]
        public void T009_WhenIReadAConfigFileContainingQuotedVariablesTheCorrectLinesArePresentAndDeQuoted()
        {
            File.WriteAllText(@".\t009.zcf", @"[SectionA]
                                                A1 = 'b '
                                                A2 = "" c """);
            ZConfigFileParser zcf = new ZConfigFileParser(@".\t009.zcf");
            Configuration conf = zcf.Read();
            Assert.AreEqual(1, conf.Count, "Unexpected number of config sections");
            ConfigSection sectionA = conf["SectionA"];
            Assert.AreEqual(2, sectionA.Lines.Count, "Unexpected number of lines in Section A");
            Assert.IsTrue(sectionA.Lines.ContainsKey("A2"), "Section A does not contain variable 'A2'");
            Assert.AreEqual(" c ", sectionA.Lines["A2"], "Section A variable 'A2' did not contain value 'c'");
            Assert.IsTrue(sectionA.Lines.ContainsKey("A1"), "Section A does not contain variable 'A1'");
            Assert.AreEqual("b ", sectionA.Lines["A1"], "Section A variable 'A1' did not contain value 'b'");
        }

        [TestMethod]
        public void T010_WhenIReadAConfigFileContainingVariablesWithAnEqualityTheCorrectLinesArePresent()
        {
            File.WriteAllText(@".\t010.zcf", @"[SectionA]
                                                A1 = b
                                                A2 = a=c");
            ZConfigFileParser zcf = new ZConfigFileParser(@".\t010.zcf");
            Configuration conf = zcf.Read();
            Assert.AreEqual(1, conf.Count, "Unexpected number of config sections");
            ConfigSection sectionA = conf["SectionA"];
            Assert.AreEqual(2, sectionA.Lines.Count, "Unexpected number of lines in Section A");
            Assert.IsTrue(sectionA.Lines.ContainsKey("A2"), "Section A does not contain variable 'A2'");
            Assert.AreEqual("a=c", sectionA.Lines["A2"], "Section A variable 'A2' did not contain value 'c'");
            Assert.IsTrue(sectionA.Lines.ContainsKey("A1"), "Section A does not contain variable 'A1'");
            Assert.AreEqual("b", sectionA.Lines["A1"], "Section A variable 'A1' did not contain value 'b'");
        }

    }
}
