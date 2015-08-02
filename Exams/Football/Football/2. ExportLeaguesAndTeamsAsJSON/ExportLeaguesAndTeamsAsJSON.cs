namespace _2.ExportLeaguesAndTeamsAsJSON
{
    using System.Linq;
    using _1.EntityFrameworkMappingsDBFirst;
    using System.Web.Script.Serialization;

    class ExportLeaguesAndTeamsAsJson
    {
        static void Main()
        {
            var context = new FootballEntities();
            JavaScriptSerializer js = new JavaScriptSerializer();

            var leagueAndTeams = context.Leagues
                .OrderBy(l => l.LeagueName)
                .Select(l => new
            {
                leagueName = l.LeagueName,
                teams = l.Teams
                    .OrderBy(t => t.TeamName)
                    .Select(t => t.TeamName)
            });

            var json = js.Serialize(leagueAndTeams);
            System.IO.File.WriteAllText(@"..\..\characters.json", json);
        }
    }
}
