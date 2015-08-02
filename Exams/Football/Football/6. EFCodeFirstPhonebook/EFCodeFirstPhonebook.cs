namespace _6.EFCodeFirstPhonebook
{
    using System;
    using System.Linq;

    class EFCodeFirstPhonebook
    {
        static void Main()
        {
            var context = new PhonebookContext();

            var contacts = context.Contacts
                .Select(c => new
                {
                    c.Name,
                    c.Note,
                    c.Position,
                    c.Url,
                    c.Company,
                    Emails = c.Emails.Select(e => e.EmailAdress),
                    Phones = c.Phones.Select(p => p.PhoneNumber)
                });

            foreach (var contact in contacts)
            {
                Console.WriteLine("Name: {0}", contact.Name);
                if (contact.Position != null)
                {
                    Console.WriteLine("  Position: {0}", contact.Position);
                }

                if (contact.Company != null)
                {
                    Console.WriteLine("  Company: {0}", contact.Company);
                }

                if (contact.Emails.Count() != 0)
                {
                    string emails = String.Join(", ", contact.Emails.ToList());
                    Console.WriteLine("  Emails: {0}", emails);
                }

                if (contact.Phones.Count() != 0)
                {
                    string phones = String.Join(", ", contact.Phones.ToList());
                    Console.WriteLine("  Phones: {0}", phones);
                }

                if (contact.Url != null)
                {
                    Console.WriteLine("  URL: {0}", contact.Url);
                }

                if (contact.Note != null)
                {
                    Console.WriteLine("  Notes: {0}", contact.Note);
                }
            }
        }
    }
}
