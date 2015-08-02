using System.Data.Entity;
using System.Linq;
using System.Xml.XPath;

namespace _4.ImportLeaguesAndTeamsFromXML
{
    using _1.EntityFrameworkMappingsDBFirst;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using System.Xml;
    using System.Xml.Linq;

    class ImportLeaguesAndTeamsFromXml
    {
        static void Main(string[] args)
        {

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var context = new FootballEntities();

            XDocument xmlDocument = XDocument.Load("../../leagues-and-teams.xml");
            var xLeagues = xmlDocument.XPathSelectElements("/leagues-and-teams/league");

            int count = 1;

            foreach (var xLeague in xLeagues)
            {
                Console.WriteLine("Processing league #{0} ...", count);
                League newLeague = CreateNewLeagueIfNotExists(context, xLeague);
                var xTeams = xLeague.XPathSelectElements("teams/team");
                CreateNewTeamsIfNotExists(context, newLeague, xTeams);
                
                count++;
            }
        }
		
        private static void CreateNewTeamsIfNotExists(FootballEntities context, League league, IEnumerable<XElement> xTeams)
        {
            foreach (var xTeam in xTeams)
            {
                var teamName = xTeam.Attribute("name").Value;
                var xCountry = xTeam.Attribute("country");
                string countryName = null;
                if (xCountry != null)
                {
                    countryName = xCountry.Value;
                }

                var team = context.Teams
                    .Include(t => t.Leagues)
                    .FirstOrDefault(t => t.TeamName == teamName 
                        && t.Country.CountryName == countryName);

                if (team != null)
                {
                    Console.WriteLine("Existing team: {0} ({1})",
                        team.TeamName, countryName ?? "no country");
                }
                else
                {
                    team = new Team()
                    {
                        TeamName = teamName,
                        Country = context.Countries.FirstOrDefault(c => c.CountryName == countryName)
                    };

                    context.Teams.Add(team);
                    context.SaveChanges();
                    Console.WriteLine("Created team: {0} ({1})",
                        team.TeamName, countryName ?? "no country");
                }

                AddTeamToLeague(context, team, league);
            }
        }

        private static void AddTeamToLeague(FootballEntities context, Team team, League league)
        {
            if (league != null)
            {
                if (team.Leagues.Contains(league))
                {
                    Console.WriteLine("Existing team in league: {0} belongs to {1}",
                        team.TeamName, league.LeagueName);
                }
                else
                {
                    team.Leagues.Add(league);
                    context.SaveChanges();
                    Console.WriteLine("Added team to league: {0} to league {1}",
                        team.TeamName, league.LeagueName);
                }
            }
        }

        private static League CreateNewLeagueIfNotExists(FootballEntities context, XElement xLeague)
        {
            League league = null;
            var xLeagueName = xLeague.Element("league-name");
            if (xLeagueName != null)
            {
                string leagueName = xLeagueName.Value;
                league = context.Leagues.FirstOrDefault(l => l.LeagueName == leagueName);
                if (league != null)
                {
                    Console.WriteLine("Existing league: {0}", xLeagueName.Value);
                }
                else
                {
                    league = new League() { LeagueName = xLeagueName.Value };
                    context.Leagues.Add(league);
                    context.SaveChanges();
                    Console.WriteLine("Created league: {0}", xLeagueName.Value);
                }
            }

            return league;
        }
    }
}
