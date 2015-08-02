namespace _6.EFCodeFirstPhonebook
{
    using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

    public class User
    {
        private ICollection<Channel> channels;

        public User()
        {
            this.channels = new HashSet<Channel>();
        }

        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public virtual ICollection<Channel> Channels { get; set; }
    }
}
