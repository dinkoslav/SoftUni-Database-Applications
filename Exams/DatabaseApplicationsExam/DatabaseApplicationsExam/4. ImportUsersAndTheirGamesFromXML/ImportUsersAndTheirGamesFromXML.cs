using System.Linq;

namespace _4.ImportUsersAndTheirGamesFromXML
{
    using _1.EntityFrameworkMappingsDBFirst;
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using System.Xml.XPath;

    class ImportUsersAndTheirGamesFromXML
    {
        static void Main(string[] args)
        {
            var context = new DiabloEntities();

            XDocument xmlDocument = XDocument.Load("../../users-and-games.xml");
            var xUsers = xmlDocument.XPathSelectElements("/users/user");

            foreach (var xUser in xUsers)
            {
                User newUser = CreateNewUserIfNotExists(context, xUser);
                var xGames = xUser.XPathSelectElements("games/game");
                CreateNewGameIfNotExists(context, newUser, xGames);
            }
        }

        private static void CreateNewGameIfNotExists(DiabloEntities context, User user, IEnumerable<XElement> xGames)
        {
            foreach (var xGame in xGames)
            {
                var gameName = xGame.Element("game-name").Value;
                var characterName = xGame.Element("character").Attribute("name").Value;
                var characterCash = decimal.Parse(xGame.Element("character").Attribute("cash").Value);
                var characterLevel = int.Parse(xGame.Element("character").Attribute("level").Value);
                var characterJoinedOn = DateTime.Parse(xGame.Element("joined-on").Value);
                
                var game = context.Games
                    .FirstOrDefault(g => g.Name == gameName);

                if (game == null)
                {
                    game = new Game()
                    {
                        Name = gameName
                    };

                    context.Games.Add(game);
                }

                if (user != null)
                {
                    if (game.UsersGames.All(ug => ug.User.Username != user.Username))
                    {
                        UsersGame userGame = new UsersGame()
                        {
                            JoinedOn = characterJoinedOn,
                            Cash = characterCash,
                            Level = characterLevel,
                            Character = new Character() { Name = characterName },
                            Game = game,
                            User = user
                        };

                        context.UsersGames.Add(userGame);
                        Console.WriteLine("User {0} successfully added to game {1}",
                        user.Username, game.Name);
                    }
                }
            }

            try
            {
                context.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("There was a problem adding user in games");
            }
            
        }

        private static User CreateNewUserIfNotExists(DiabloEntities context, XElement xUser)
        {
            User user = null;
            var xUserUsername = xUser.Attribute("username");
            var xUserIpAddress = xUser.Attribute("ip-address");
            var xUserIsDeleted = xUser.Attribute("is-deleted");
            var xUserRegistrationDate = xUser.Attribute("registration-date");

            if (xUserUsername != null &&
                xUserRegistrationDate != null &&
                xUserIpAddress != null &&
                xUserIsDeleted != null)
            {
                string userUsername = xUserUsername.Value;
                user = context.Users.FirstOrDefault(u => u.Username == userUsername);
                if (user != null)
                {
                    Console.WriteLine("User {0} already exists", userUsername);
                }
                else
                {
                    user = new User()
                    {
                        Username = userUsername,
                        IpAddress = xUserIpAddress.Value,
                        IsDeleted = xUserIsDeleted.Value != "0",
                        RegistrationDate = DateTime.Parse(xUserRegistrationDate.Value)
                    };

                    if (xUser.Attribute("first-name") != null)
                    {
                        user.FirstName = xUser.Attribute("first-name").Value;
                    }

                    if (xUser.Attribute("last-name") != null)
                    {
                        user.LastName = xUser.Attribute("last-name").Value;
                    }

                    if (xUser.Attribute("email") != null)
                    {
                        user.Email = xUser.Attribute("email").Value;
                    }

                    context.Users.Add(user);
                    Console.WriteLine("Successfully added user {0}", userUsername);
                }
            }

            return user;
        }
    }
}
