using System.Collections.Generic;
using System.Linq;

namespace NRanking.Server.Classes
{
    public class Tournament
    {
        public string TournamentName;
        public IList<Player> Players = new List<Player>();
        public IList<TournamentResult> Results = new List<TournamentResult>();
        public IList<Round> Rounds = new List<Round>();
        public string Country;
        public string TournamentDate;
        public int RankingNumber;

        public Round GetRound(int roundNumber)
        {
            if (Rounds.Where(r => r.RoundNumber == roundNumber).Any())
                return Rounds.Single(r => r.RoundNumber == roundNumber);

            var round = new Round
                        {
                            RoundNumber = roundNumber,
                            Tournament = this,                               
                        };
            Rounds.Add(round);
            return round;
            
        }

        public Player GetPlayerById(int blackPlayerId)
        {
            return Players.SingleOrDefault(p => p.PlayerId == blackPlayerId);
        }
    }
}
