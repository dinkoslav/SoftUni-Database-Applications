namespace _1.EntityFrameworkMappingsDBFirst
{
    class EntityFrameworkMappings
    {
        static void Main()
        {
            var context = new FootballEntities();

            var teams = context.Teams;

            foreach (var team in teams)
            {
                System.Console.WriteLine(team.TeamName);
            }
        }
    }
}
