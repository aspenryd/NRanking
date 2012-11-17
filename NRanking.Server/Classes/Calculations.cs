using System;
using System.Linq;

namespace NRanking.Server.Classes
{
    public class CalculationException : Exception
    {
        public CalculationException(string message)
            : base(message)
        {

        }
    }

    public class Calculations
    {
        #region Publika metoder

        public void CalculateTournament(ref Tournament tournament)
        {
            foreach (var game in tournament.Rounds.SelectMany(round => round.Games))
            {
                CalculateRankingDiff(game);
            }
        }

        public int CalculateRankingDiff(Game game)
        {
            return CalculateRankingDiff(game.BlackPlayer, game.WhitePlayer, game.BlackScore, game.WhiteScore);
        }

        public int CalculateRankingDiff(Player blackPlayer, Player whitePlayer, int blackScore)
        {
            return CalculateRankingDiff(blackPlayer, whitePlayer, blackScore, 64-blackScore);
        }

        public int CalculateRankingDiff(Player blackPlayer, Player whitePlayer, int blackScore, int whiteScore)
        {
            var diff = CalculateRankingDiff(blackPlayer.CurrentRanking, whitePlayer.CurrentRanking, blackScore, whiteScore);
            blackPlayer.RankDiff += diff;
            whitePlayer.RankDiff -= diff;
            return diff;
        }

        public int CalculateRankingDiff(int blackRanking, int whiteRanking, int blackScore, int whiteScore)
        {
            if (blackScore + whiteScore != 64) throw new CalculationException(string.Format("White and black score doesnt add up to 64. Black:{0} White:{1}", blackScore, whiteScore));
            if (blackRanking < 0 || whiteRanking < 0) throw new CalculationException(string.Format("Rating must be positive. Black:{0} White:{1}", blackRanking, whiteRanking));

            return CalculateRankingDiff(blackRanking - whiteRanking, blackScore - whiteScore);
        }


        #endregion

        #region Privata metoder

        private static int GetValueSpan(int scoreDiff)
        {
            if (scoreDiff == 0) return 0;
            if (scoreDiff == 2) return 1;
            if (scoreDiff >= 4 && scoreDiff <= 10) return 2;
            if (scoreDiff >= 11 && scoreDiff <= 20) return 3;
            if (scoreDiff >= 21 && scoreDiff <= 30) return 4;
            if (scoreDiff >= 31 && scoreDiff <= 40) return 5;
            if (scoreDiff >= 41 && scoreDiff <= 50) return 6;
            if (scoreDiff >= 51 && scoreDiff <= 60) return 7;
            if (scoreDiff >= 61 && scoreDiff <= 64) return 8;
            throw new CalculationException(string.Format("Score diff outside of valid span: {0}", scoreDiff));
        }

        private static int[] GetRankingDiffTable(int rankingDiff)
        {
            if (rankingDiff >= 351) return new[] { -16, -6, -4, -2, 0, 2, 4, 6, 7 };
            if (rankingDiff >= 301) return new[] { -15, -4, -3, -1, 1, 3, 5, 7, 8 };
            if (rankingDiff >= 251) return new[] { -13, -3, -1, 1, 3, 5, 7, 9, 10 };
            if (rankingDiff >= 201) return new[] { -11, -1, 1, 3, 5, 7, 9, 11, 12 };
            if (rankingDiff >= 151) return new[] { -9, -1, 3, 5, 7, 9, 11, 13, 14 };
            if (rankingDiff >= 101) return new[] { -7, 3, 5, 7, 9, 11, 13, 15, 16 };
            if (rankingDiff >= 51) return new[] { -5, 6, 7, 9, 11, 13, 15, 17, 18 };
            if (rankingDiff >= 1) return new[] { -2, 8, 10, 12, 14, 16, 18, 20, 20 };
            if (rankingDiff == 0) return new[] { 0, 10, 12, 14, 16, 18, 20, 22, 23 };
            if (rankingDiff >= -50) return new[] { 2, 13, 14, 16, 18, 20, 22, 24, 25 };
            if (rankingDiff >= -100) return new[] { 5, 15, 17, 19, 21, 23, 25, 27, 28 };
            if (rankingDiff >= -150) return new[] { 7, 17, 19, 21, 23, 25, 27, 29, 30 };
            if (rankingDiff >= -200) return new[] { 9, 20, 21, 23, 25, 27, 29, 31, 32 };
            if (rankingDiff >= -250) return new[] { 11, 22, 23, 25, 27, 29, 31, 33, 35 };
            if (rankingDiff >= -300) return new[] { 15, 23, 25, 27, 29, 31, 33, 35, 36 };
            if (rankingDiff >= -350) return new[] { 15, 25, 27, 29, 31, 33, 35, 37, 37 };
            return new[] { 16, 27, 28, 30, 32, 34, 36, 38, 39 };
        }

        private static int CalculateRankingDiff(int rankingDiff, int scoreDiff)
        {
            var rankDiff = GetRankingDiffTable(rankingDiff)[GetValueSpan(Math.Abs(scoreDiff))];
            return scoreDiff >= 0 ? rankDiff : -rankDiff;
        }


        #endregion

    }

    
}
