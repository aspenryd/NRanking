using NRanking.Server.FileHandlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NRanking.Server.Test.Classes
{
    [TestClass]
    public class EloFileTests
    {
        [TestMethod]
        public void LoadFile_CorrectFile_NoProblems()
        {
            const string fileName = @"..\..\..\testFiles\testfile.elo";
            var eloFile = new EloFile(fileName);
            Assert.IsNotNull(eloFile.Tournament);
            Assert.AreEqual(11, eloFile.Tournament.Rounds.Count, "Rounds");
            Assert.AreEqual(11, eloFile.Tournament.Rounds[0].Games.Count, "Games for round 1");
            Assert.AreEqual(22, eloFile.Tournament.Results.Count, "Results");
            Assert.AreEqual(22, eloFile.Tournament.Players.Count, "Players");
        }

        [TestMethod]
        public void StringHelper_PartToString_Tests()
        {
            Assert.AreEqual("abcdef", EloFile.PartToString("abcdef", 1));
            Assert.AreEqual("cdef", EloFile.PartToString("abcdef", 3));
            Assert.AreEqual("f", EloFile.PartToString("abcdef", 6));
            Assert.AreEqual("def", EloFile.PartToString("ab def ", 3));
        }

        [TestMethod]
        public void StringHelper_PartToInt_Tests()
        {            
            Assert.AreEqual(23, EloFile.PartToInt("abc1234jk", 5, 2));
            Assert.AreEqual(56, EloFile.PartToInt("abc 567jk", 4, 3));
            Assert.AreEqual(45, EloFile.PartToInt("abc 45 jk", 4, 4));

            Assert.AreEqual(1234, EloFile.PartToInt("abc1234", 4));
            Assert.AreEqual(234, EloFile.PartToInt("abc 234", 4));
            Assert.AreEqual(34, EloFile.PartToInt("abc 234 ", 6));
        }

        [TestMethod]
        public void StringHelper_ConvertToDouble_Tests()
        {
            Assert.AreEqual(12.34, EloFile.ConvertToDouble("12.34"));
            Assert.AreEqual(23.45, EloFile.ConvertToDouble("23,45"));
            Assert.AreEqual(34.56, EloFile.ConvertToDouble(" 34,56 "));
        }

        [TestMethod]
        public void StringHelper_ConvertToInt_Tests()
        {
            Assert.AreEqual(12, EloFile.ConvertToInt("12"));
            Assert.AreEqual(13, EloFile.ConvertToDouble("13 "));
            Assert.AreEqual(14, EloFile.ConvertToDouble(" 14"));
        }

    }
}