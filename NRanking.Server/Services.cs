using NRanking.Server.Classes;
using NRanking.Server.FileHandlers;

namespace NRanking.Server
{
    public class Services
    {
        public Tournament RankTournament(string fileName)
        {
            var tournament =  new EloFile(fileName).Tournament;
            new Calculations().CalculateTournament(ref tournament);
            return tournament;
        }
    }
}
