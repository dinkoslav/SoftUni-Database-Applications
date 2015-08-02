namespace _6.EFCodeFirstPhonebook.Migrations
{
    using System;
    using System.Collections.Generic;
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
            if (!context.Contacts.Any())
            {
                Contact contact1 = new Contact()
                {
                    Name = "Peter Ivanov",
                    Position = "CTO",
                    Company = "Smart Ideas",
                    Url = "http://blog.peter.com",
                    Note = "Friend from school",
                    Emails = new List<Email>()
                    {
                        new Email()
                        {
                            EmailAdress = "peter@gmail.com"
                        },
                        new Email()
                        {
                            EmailAdress = "peter_ivanov@yahoo.com"
                        }
                    },
                    Phones = new List<Phone>()
                    {
                        new Phone()
                        {
                            PhoneNumber = "+359 2 22 22 22"
                        },
                        new Phone()
                        {
                            PhoneNumber = "+359 88 77 88 99"
                        }
                    }
                };
                context.Contacts.Add(contact1);

                Contact contact2 = new Contact()
                {
                    Name = "Maria",
                    Phones = new List<Phone>()
                    {
                        new Phone()
                        {
                            PhoneNumber = "+359 22 33 44 55"
                        }
                    }
                };
                context.Contacts.Add(contact2);

                Contact contact3 = new Contact()
                {
                    Name = "Angie Stanton",
                    Url = "http://angiestanton.com",
                    Emails = new List<Email>()
                    {
                        new Email()
                        {
                            EmailAdress = "info@angiestanton.com"
                        }
                    }
                };
                context.Contacts.Add(contact3);
                context.SaveChanges();
            }
        }
    }
}
