using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }

        public string? ImageBase64 { get; set; }

        [Required]
        public string? Description { get; set; }

    }
}
