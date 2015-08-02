using System.Linq;

namespace _2.ExportCharactersAndPlayersAsJSON
{
    using _1.EntityFrameworkMappingsDBFirst;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Script.Serialization;

    class ExportCharactersAndPlayersAsJSON
    {
        static void Main()
        {
            var context = new DiabloEntities();
            JavaScriptSerializer js = new JavaScriptSerializer();

            var characters = context.Characters
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    name = c.Name,
                    playedBy = c.UsersGames
                        .Select(u => u.User.Username)
                });

            var json = js.Serialize(characters);
            File.WriteAllText(@"..\..\characters.json", json);
        }
    }
}
