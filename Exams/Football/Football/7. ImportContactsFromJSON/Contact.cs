namespace _7.ImportContactsFromJSON
{
    using System.Collections.Generic;

    public class Contact
    {
        public string Name { get; set; }

        public string Company { get; set; }

        public string Position { get; set; }

        public string Site { get; set; }

        public string Notes { get; set; }

        public List<string> Emails { get; set; }

        public List<string> Phones { get; set; }

    }
}
