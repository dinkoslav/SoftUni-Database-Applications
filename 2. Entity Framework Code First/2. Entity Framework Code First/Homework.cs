namespace _2.Entity_Framework_Code_First
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Homework
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public ContentType ContentType { get; set; }

        [Required]
        public DateTime SubmissionDate { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public virtual Student Student { get; set; }
    }
}
