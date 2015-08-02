namespace _6.EFCodeFirstPhonebook.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PhonebookContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "_6.EFCodeFirstPhonebook.PhonebookContext";

        }

        protected override void Seed(PhonebookContext context)
        {
            if (!context.Users.Any())
            {
                User user1 = new User()
                {
                    Username = "VGeorgiev",
                    FullName = "Vladimir Georgiev",
                    PhoneNumber = "0894545454"
                };

                context.Users.Add(user1);

                User user2 = new User()
                {
                    Username = "Nakov",
                    FullName = "Svetlin Nakov",
                    PhoneNumber = "0897878787"
                };

                context.Users.Add(user2);

                User user3 = new User()
                {
                    Username = "Ache",
                    FullName = "Angel Georgiev",
                    PhoneNumber = "0897121212"
                };

                context.Users.Add(user3);

                User user4 = new User()
                {
                    Username = "Alex",
                    FullName = "Alexandra Svilarova",
                    PhoneNumber = "0894151417"
                };

                context.Users.Add(user4);

                User user5 = new User()
                {
                    Username = "Petya",
                    FullName = "Petya Grozdarska",
                    PhoneNumber = "0895464646"
                };

                context.Users.Add(user5);

                Channel channel1 = new Channel()
                {
                    Name = "Malinki"
                };

                context.Channels.Add(channel1);

                Channel channel2 = new Channel()
                {
                    Name = "SoftUni"
                };

                context.Channels.Add(channel2);

                Channel channel3 = new Channel()
                {
                    Name = "Admins"
                };

                context.Channels.Add(channel3);

                Channel channel4 = new Channel()
                {
                    Name = "Programmers"
                };

                context.Channels.Add(channel4);

                Channel channel5 = new Channel()
                {
                    Name = "Geeks"
                };

                context.Channels.Add(channel5);

                ChannelMessage channelMessage1 = new ChannelMessage()
                {
                    Content = "Hey dudes, are you ready for tonight?"
                };
            }
        }
    }
}
