using System.Linq;

namespace _7.ImportContactsFromJSON
{
    using _6.EFCodeFirstPhonebook;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Script.Serialization;

    class ImportContactsFromJson
    {
        static void Main()
        {
            var context = new PhonebookContext();


            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            var contacts = jsSerializer.Deserialize<List<Contact>>(File.ReadAllText(@"../../contacts.json"));

            foreach (var contact in contacts)
            {
                if (String.IsNullOrEmpty(contact.Name))
                {
                    Console.WriteLine("Error: Name is required");
                    continue;
                }

                _6.EFCodeFirstPhonebook.Contact newContact = new _6.EFCodeFirstPhonebook.Contact();
                newContact.Name = contact.Name;
                if (!String.IsNullOrEmpty(contact.Position))
                {
                    newContact.Position = contact.Position;
                }

                if (!String.IsNullOrEmpty(contact.Company))
                {
                    newContact.Company = contact.Company;
                }

                if (!String.IsNullOrEmpty(contact.Site))
                {
                    newContact.Url = contact.Site;
                }

                if (!String.IsNullOrEmpty(contact.Notes))
                {
                    newContact.Note = contact.Notes;
                }

                if (contact.Emails != null)
                {
                    foreach (var email in contact.Emails)
                    {
                        newContact.Emails.Add(new Email()
                        {
                            EmailAdress = email
                        });
                    }
                }

                if (contact.Phones != null)
                {
                    foreach (var phone in contact.Phones)
                    {
                        newContact.Phones.Add(new Phone()
                        {
                            PhoneNumber = phone
                        });
                    }
                }

                Console.WriteLine("Contact {0} imported", contact.Name);
                context.Contacts.Add(newContact);
                context.SaveChanges();
            }
        }
    }
}
