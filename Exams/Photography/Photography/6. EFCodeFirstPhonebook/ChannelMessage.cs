namespace _6.EFCodeFirstPhonebook
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ChannelMessage
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime datetime { get; set; }

        [Required]
        public int ChannelId { get; set; }

        public virtual Channel Channel { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
