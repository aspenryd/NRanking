using System;

namespace NRanking.Server.Classes
{
    public class Player
    {
        public int PlayerId;
        public string FirstName;
        public string LastName;
        public int CurrentRanking;
        public Country Country;
        public bool IsNordicPlayer { get { return Country.IsNordic; } }

        public int RankDiff;
        public int NewRanking
        {
            get {return RankDiff+ CurrentRanking;}
        }
    
    }
}
