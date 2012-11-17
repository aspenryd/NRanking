using System;
using System.Globalization;
using System.IO;
using NRanking.Server.Classes;

namespace NRanking.Server.FileHandlers
{
    public class EloFile
    {
        #region Help classes

        private enum EloFileReadStatus
        {
            Started,
            Players,
            Rounds,
            AddedResults
        }

        internal class ParsingExeption : Exception
        {
            public ParsingExeption(string format)
                : base(format)
            {
            }
        }

        #endregion

        #region Public properties

        public Tournament Tournament = new Tournament();
        public string Sender;

        #endregion

        #region Private fields

        private EloFileReadStatus _status;
        private int _roundBeingParsed;
        
        
        #endregion

        #region Constructor

        public EloFile()
        {
            
        }

        public EloFile(string fileName)
        {
            ParseFile(fileName);            
        }

        #endregion

        #region Public methods

        public void ParseFile(string fileName)
        {
            _status = EloFileReadStatus.Started;
            _roundBeingParsed = 0;
            var fi = new FileInfo(fileName);
            var fs = fi.OpenText();
            while (!fs.EndOfStream)
            {
                var line = fs.ReadLine();
                ParseLine(line);
            }
        }

        #endregion

        #region Private methods

        private void ParseLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line)) return;
            if (line.StartsWith("%%"))
            {
                ParseCommentLine(line);
            } else if (line.StartsWith("% "))
            {
                ParseHeaderLine(line);
            } else if (line.StartsWith("%_%"))
            {
                ParsePlayerLine(line);
            }
            else if (line.StartsWith("%Round:"))
            {
                _status = EloFileReadStatus.Rounds;
                _roundBeingParsed = PartToInt(line, 9);
            }
            else
            {
                ParseResultLine(line);
            }
        }

        private void ParseResultLine(string line)
        {
            if (_status != EloFileReadStatus.Rounds) throw new ParsingExeption(string.Format("Trying to parse result line while having wrong status:{0} - read line {1}", _status, line));
            //  1301 (53)>(11) 150006 B
            var blackPlayerId = PartToInt(line, 1, 6);
            var blackScore = PartToInt(line, 9, 2);
            //var resultSign = line.Skip(12).Take(1).ToString().Trim();
            var whiteScore = PartToInt(line, 14, 2);
            var whitePlayerId = PartToInt(line, 18, 6);
            var round = Tournament.GetRound(_roundBeingParsed);
            var game = new Game
                           {
                               BlackPlayer = Tournament.GetPlayerById(blackPlayerId),
                               WhitePlayer = Tournament.GetPlayerById(whitePlayerId),
                               BlackScore = blackScore,
                               WhiteScore = whiteScore,
                               Round = round,
                           };
            round.Games.Add(game);
        }

        private void ParsePlayerLine(string line)
        {
            if (_status != EloFileReadStatus.Players) throw new ParsingExeption(string.Format("Trying to parse player line while having wrong status:{0} - read line {1}", _status, line));
            //%_%    5198, VAN DEN BERG, Erwin, NL,  7, 393
            line = PartToString(line, 4);
            if (line.StartsWith("+"))
                line = PartToString(line, 2);
            var values = line.Split(',');
            var player = new Player
                             {
                                 PlayerId = ConvertToInt(values[0]),
                                 LastName = values[1].Trim(),
                                 FirstName = values[2].Trim(),
                                 Country = new Country(values[3].Trim())
                             };
            Tournament.Players.Add(player);
            var tournamentResult = new TournamentResult
                                       {
                                           PlayerId = player.PlayerId,
                                           Points = ConvertToDouble(values[4]),
                                           Mbq = ConvertToDouble(values[5])
                                       };
            Tournament.Results.Add(tournamentResult);
        }

        private void ParseHeaderLine(string line)
        {
            if (line == "%        ID, NAME, Firstname, COUNTRY, score, disc-count")
                _status = EloFileReadStatus.Players;
            else throw new ParsingExeption(string.Format("Unknown Header line: {0}", line));
        }

        private void ParseCommentLine(string line)
        {
            //%%Tournament: Stockholm EGP
            //%%Country: Sweden
            //%%Date: 12/06/2010
            //%%Sender: Henry

            if(line.StartsWith("%%Tournament:"))
            {
                Tournament.TournamentName = PartToString(line, "%%Tournament:".Length+1);
            }
            else if (line.StartsWith("%%Country:"))
            {
                Tournament.Country = PartToString(line, "%%Country:".Length+1);
            }
            else if (line.StartsWith("%%Date:"))
            {
                Tournament.TournamentDate = PartToString(line, "%%Date:".Length+1);
            }
            else if (line.StartsWith("%%Sender:"))
            {
                Sender = PartToString(line, "%%Sender:".Length + 1);
            }
            else if (line.StartsWith("%%Added"))
            {
                _status = EloFileReadStatus.AddedResults;
            }
        }

        #endregion

        #region Help methods

        public static string PartToString(string s, int start)
        {
            return s.Substring(start - 1, s.Length - start + 1).Trim();
        }

        public static double ConvertToDouble(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return 0;
            var decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator[0];            
            return Double.Parse(s.Trim().Replace('.', decimalSeparator).Replace(',', decimalSeparator));
        }

        public static int ConvertToInt(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return 0;
            return Int32.Parse(s.Trim());
        }

        public static int PartToInt(string s, int start, int length)
        {
            return Int32.Parse(s.Substring(start - 1, length).Trim());
        }

        public static int PartToInt(string s, int start)
        {
            return Int32.Parse(s.Substring(start - 1, s.Length - start + 1).Trim());
        }

        #endregion
    
    }

}
