using NRanking.Server.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NRanking.Server.Test.Classes
{
    [TestClass]
    public class CalculationsTests
    {
        private readonly Calculations calculations = new Calculations();
        
        [TestMethod]
        public void TestCalculation_EqualOpponentsDraw_NoRankingDiff()
        {
            var result = calculations.CalculateRankingDiff(1000, 1000, 32, 32);
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void TestCalculation_EqualOpponentDifferentScoresBlackWins_CorrectRankingDiff()
        {
            Assert.AreEqual( 0, calculations.CalculateRankingDiff(1000, 1000, 32, 32));
            Assert.AreEqual(10, calculations.CalculateRankingDiff(1000, 1000, 33, 31));
            Assert.AreEqual(12, calculations.CalculateRankingDiff(1000, 1000, 34, 30));
            Assert.AreEqual(12, calculations.CalculateRankingDiff(1000, 1000, 37, 27));
            Assert.AreEqual(14, calculations.CalculateRankingDiff(1000, 1000, 38, 26));
            Assert.AreEqual(14, calculations.CalculateRankingDiff(1000, 1000, 42, 22));
            Assert.AreEqual(16, calculations.CalculateRankingDiff(1000, 1000, 43, 21));
            Assert.AreEqual(16, calculations.CalculateRankingDiff(1000, 1000, 47, 17));
            Assert.AreEqual(18, calculations.CalculateRankingDiff(1000, 1000, 48, 16));
            Assert.AreEqual(18, calculations.CalculateRankingDiff(1000, 1000, 52, 12));
            Assert.AreEqual(20, calculations.CalculateRankingDiff(1000, 1000, 53, 11));
            Assert.AreEqual(20, calculations.CalculateRankingDiff(1000, 1000, 57, 7));
            Assert.AreEqual(22, calculations.CalculateRankingDiff(1000, 1000, 58, 6));
            Assert.AreEqual(22, calculations.CalculateRankingDiff(1000, 1000, 62, 2));
            Assert.AreEqual(23, calculations.CalculateRankingDiff(1000, 1000, 63, 1));
            Assert.AreEqual(23, calculations.CalculateRankingDiff(1000, 1000, 64, 0));
        }

        [TestMethod]
        public void TestCalculation_EqualOpponentDifferentScoresWhiteWins_CorrectRankingDiff()
        {
            Assert.AreEqual(0, calculations.CalculateRankingDiff(1000, 1000, 32, 32));
            Assert.AreEqual(-10, calculations.CalculateRankingDiff(1000, 1000, 31, 33));
            Assert.AreEqual(-12, calculations.CalculateRankingDiff(1000, 1000, 30, 34));
            Assert.AreEqual(-12, calculations.CalculateRankingDiff(1000, 1000, 27, 37));
            Assert.AreEqual(-14, calculations.CalculateRankingDiff(1000, 1000, 26, 38));
            Assert.AreEqual(-14, calculations.CalculateRankingDiff(1000, 1000, 22, 42));
            Assert.AreEqual(-16, calculations.CalculateRankingDiff(1000, 1000, 21, 43));
            Assert.AreEqual(-16, calculations.CalculateRankingDiff(1000, 1000, 17, 47));
            Assert.AreEqual(-18, calculations.CalculateRankingDiff(1000, 1000, 16, 48));
            Assert.AreEqual(-18, calculations.CalculateRankingDiff(1000, 1000, 12, 52));
            Assert.AreEqual(-20, calculations.CalculateRankingDiff(1000, 1000, 11, 53));
            Assert.AreEqual(-20, calculations.CalculateRankingDiff(1000, 1000, 7, 57));
            Assert.AreEqual(-22, calculations.CalculateRankingDiff(1000, 1000, 6, 58));
            Assert.AreEqual(-22, calculations.CalculateRankingDiff(1000, 1000, 2, 62));
            Assert.AreEqual(-23, calculations.CalculateRankingDiff(1000, 1000, 1, 63));
            Assert.AreEqual(-23, calculations.CalculateRankingDiff(1000, 1000, 0, 64));
        }

        [TestMethod]
        public void TestCalculation_GenericTestOfCalculation_CorrectRankingDiff()
        {
            VerifyRankdiff(1351, 1000, new int[] { -16, -6, -4, -2, 0, 2, 4, 6, 7 });
            VerifyRankdiff(1301, 1000, new int[] { -15, -4, -3, -1, 1, 3, 5, 7, 8 });
            VerifyRankdiff(1251, 1000, new int[] { -13, -3, -1, 1, 3, 5, 7, 9, 10 });
            VerifyRankdiff(1251, 1000, new int[] { -13, -3, -1, 1, 3, 5, 7, 9, 10 });
            VerifyRankdiff(1201, 1000, new int[] { -11, -1, 1, 3, 5, 7, 9, 11, 12 });
            VerifyRankdiff(1151, 1000, new int[] { -9, -1, 3, 5, 7, 9, 11, 13, 14 });
            VerifyRankdiff(1101, 1000, new int[] { -7, 3, 5, 7, 9, 11, 13, 15, 16 });
            VerifyRankdiff(1051, 1000, new int[] { -5, 6, 7, 9, 11, 13, 15, 17, 18 });
            VerifyRankdiff(1001, 1000, new int[] { -2, 8, 10, 12, 14, 16, 18, 20, 20 });
            VerifyRankdiff(1000, 1000, new int[] { 0, 10, 12, 14, 16, 18, 20, 22, 23 });
            VerifyRankdiff(1000-50 , 1000, new int[] { 2, 13, 14, 16, 18, 20, 22, 24, 25 });
            VerifyRankdiff(1000-100, 1000, new int[] { 5, 15, 17, 19, 21, 23, 25, 27, 28 });
            VerifyRankdiff(1000-150, 1000, new int[] { 7, 17, 19, 21, 23, 25, 27, 29, 30 });
            VerifyRankdiff(1000-200, 1000, new int[] { 9, 20, 21, 23, 25, 27, 29, 31, 32 }); 
            VerifyRankdiff(1000-250, 1000, new int[] { 11, 22, 23, 25, 27, 29, 31, 33, 35 });
            VerifyRankdiff(1000-300, 1000, new int[] { 15, 23, 25, 27, 29, 31, 33, 35, 36 });
            VerifyRankdiff(1000-350, 1000, new int[] { 15, 25, 27, 29, 31, 33, 35, 37, 37 });
            VerifyRankdiff(1000-351, 1000, new int[] { 16, 27, 28, 30, 32, 34, 36, 38, 39 });
        }

        private void VerifyRankdiff(int blackrank, int whiterank, int[] discdifflista)
        {
            Assert.AreEqual(9, discdifflista.Length, "Discdifflist must contain 9 elements, no more, no less. (black:{0}, white:{1}, list:{2})", blackrank, whiterank, discdifflista);
            Assert.AreEqual(discdifflista[0], calculations.CalculateRankingDiff(blackrank, whiterank, 32, 32), "(black:{0}, white:{1}, list:{2})", blackrank, whiterank, discdifflista);
            Assert.AreEqual(discdifflista[1], calculations.CalculateRankingDiff(blackrank, whiterank, 33, 31), "(black:{0}, white:{1}, list:{2})", blackrank, whiterank, discdifflista);
            Assert.AreEqual(discdifflista[2], calculations.CalculateRankingDiff(blackrank, whiterank, 37, 27), "(black:{0}, white:{1}, list:{2})", blackrank, whiterank, discdifflista);
            Assert.AreEqual(discdifflista[3], calculations.CalculateRankingDiff(blackrank, whiterank, 42, 22), "(black:{0}, white:{1}, list:{2})", blackrank, whiterank, discdifflista);
            Assert.AreEqual(discdifflista[4], calculations.CalculateRankingDiff(blackrank, whiterank, 47, 17), "(black:{0}, white:{1}, list:{2})", blackrank, whiterank, discdifflista);
            Assert.AreEqual(discdifflista[5], calculations.CalculateRankingDiff(blackrank, whiterank, 52, 12), "(black:{0}, white:{1}, list:{2})", blackrank, whiterank, discdifflista);
            Assert.AreEqual(discdifflista[6], calculations.CalculateRankingDiff(blackrank, whiterank, 57, 7), "(black:{0}, white:{1}, list:{2})", blackrank, whiterank, discdifflista);
            Assert.AreEqual(discdifflista[7], calculations.CalculateRankingDiff(blackrank, whiterank, 62, 2), "(black:{0}, white:{1}, list:{2})", blackrank, whiterank, discdifflista);
            Assert.AreEqual(discdifflista[8], calculations.CalculateRankingDiff(blackrank, whiterank, 64, 0), "(black:{0}, white:{1}, list:{2})", blackrank, whiterank, discdifflista);
        }
    }
}
