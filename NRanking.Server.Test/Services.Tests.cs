using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NRanking.Server.Test
{
    [TestClass]
    public class ServicesTest
    {
        [TestMethod]
        public void Calculation_RankTournamentFromELOFile_NoProblems()
        {
            const string fileName = @"..\..\..\testFiles\testfile.elo";
            var tournament = new Services().RankTournament(fileName);
            
            Assert.AreEqual(22, tournament.Players.Count, "Players");
            Assert.AreEqual(0, tournament.Players.Sum(p=>p.RankDiff), "Rankdiff");
            Assert.AreEqual(114, tournament.Players.Max(p => p.RankDiff), "RankdiffMax");
            Assert.AreEqual(-203, tournament.Players.Min(p => p.RankDiff), "RankdiffMin");

        }
    }
}