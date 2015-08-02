using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace _6.EFCodeFirstPhonebook
{
    public class Channel
    {
        private ICollection<User> users;

        public Channel()
        {
            this.users = new HashSet<User>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
