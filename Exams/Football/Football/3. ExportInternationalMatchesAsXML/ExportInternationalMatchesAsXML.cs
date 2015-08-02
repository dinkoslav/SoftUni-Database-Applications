namespace _3.ExportInternationalMatchesAsXML
{
    using _1.EntityFrameworkMappingsDBFirst;
    using System;
    using System.Linq;
    using System.Xml.Linq;

    class ExportInternationalMatchesAsXml
    {
        static void Main()
        {
            var context = new FootballEntities();

            var matches = context.InternationalMatches
                .OrderBy(m => m.MatchDate)
                .ThenBy(m => m.HomeCountry)
                .ThenBy(m => m.AwayCountry)
                .Select(m => new
                {
                    MatchDate = m.MatchDate,
                    HomeCountry = m.HomeCountry.CountryName,
                    HomeCountryCode = m.HomeCountryCode,
                    AwayCountry = m.AwayCountry.CountryName,
                    AwayCountryCode = m.AwayCountryCode,
                    League = m.League.LeagueName,
                    Score = m.HomeGoals + "-" + m.AwayGoals
                });

            XElement matchesElement = new XElement("matches");

            foreach (var match in matches)
            {
                XElement matchElement = new XElement("match");
                if (match.MatchDate != null)
                {
                    string date = "";
                    date = ((DateTime) match.MatchDate)
                        .ToString(((DateTime)match.MatchDate).Hour != 0 ? "dd-MMM-yyyy hh:mm" : "dd-MMM-yyyy");
                    matchElement.SetAttributeValue("date", date);
                }

                XElement homeCountryElement = new XElement("home-country", match.HomeCountry);
                homeCountryElement.SetAttributeValue("code", match.HomeCountryCode);
                matchElement.Add(homeCountryElement);

                XElement awayCountryElement = new XElement("away-country", match.AwayCountry);
                awayCountryElement.SetAttributeValue("code", match.AwayCountryCode);
                matchElement.Add(awayCountryElement);

                if (match.League != null)
                {
                    XElement leagueElement = new XElement("league", match.League);
                    matchElement.Add(leagueElement);
                }

                if (match.Score != "-")
                {
                    XElement scoreElement = new XElement("score", match.Score);
                    matchElement.Add(scoreElement);
                }

                matchesElement.Add(matchElement);
            }

            var resultXmlDoc = new XDocument();
            resultXmlDoc.Add(matchesElement);
            resultXmlDoc.Save("../../international-matches.xml");
        }
    }
}
