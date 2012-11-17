using System.Linq;

namespace NRanking.Server.Classes
{
    public class Country
    {
        private static readonly string[] NordicCountryAbbrevations = new []{"S", "N", "DK", "FIN", "ISL"};

        public string Name;
        public string Abbreviation;

        public Country(string name)
        {
            if (name.Length<=3)
                Abbreviation = name;
            else
                Name = name;
        }

        public bool IsNordic
        {
            get {return NordicCountryAbbrevations.Contains(Abbreviation);}
        }
    }
}
