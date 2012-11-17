using System.Collections.Generic;

namespace NRanking.Server.Classes
{
    public class Round
    {
        public int RoundNumber;
        public Tournament Tournament;
        public IList<Game> Games = new List<Game>();        
    }
}
