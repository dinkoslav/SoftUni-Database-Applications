using System.Linq;

namespace _3.ExportFinishedGamesAsXML
{
    using _1.EntityFrameworkMappingsDBFirst;
    using System;
    using System.Xml.Linq;

    class ExportFinishedGamesAsXML
    {
        static void Main()
        {
            var context = new DiabloEntities();

            var finishedGames = context.Games
                .Where(g => g.IsFinished == true)
                .OrderBy(g => g.Name)
                .ThenBy(g => g.Duration)
                .Select(g => new
                {
                    g.Name,
                    g.Duration,
                    Users = g.UsersGames
                        .Select(ug => new
                        {
                            Username = ug.User.Username,
                            Ip = ug.User.IpAddress
                        })
                });

            XElement gamesElement = new XElement("games");

            foreach (var finishedGame in finishedGames)
            {
                XElement gameElement = new XElement("game");
                gameElement.SetAttributeValue("name", finishedGame.Name);
                if (finishedGame.Duration != null)
                {
                    gameElement.SetAttributeValue("duration", finishedGame.Duration); 
                }

                XElement usersElement = new XElement("users");
                foreach (var user in finishedGame.Users)
                {
                    XElement userElement = new XElement("user");
                    userElement.SetAttributeValue("username", user.Username);
                    userElement.SetAttributeValue("ip-address", user.Ip);
                    usersElement.Add(userElement);
                }

                gameElement.Add(usersElement);
                gamesElement.Add(gameElement);
            }

            var resultXmlDoc = new XDocument();
            resultXmlDoc.Add(gamesElement);
            resultXmlDoc.Save("../../finished-games.xml");
        }
    }
}
