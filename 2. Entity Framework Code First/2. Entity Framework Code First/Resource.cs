namespace _2.Entity_Framework_Code_First
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Resource
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ResourceType Type { get; set; }

        [Required]
        public string Url { get; set; }
    }
}
