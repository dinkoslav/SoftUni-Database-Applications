namespace _6.EFCodeFirstPhonebook
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserMessage
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime datetime { get; set; }

        [Required]
        public int RecipientId { get; set; }

        public virtual User Recipient { get; set; }

        [Required]
        public int SenderId { get; set; }

        public virtual User Sender { get; set; }
    }
}
