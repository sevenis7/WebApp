using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Service
    {
        public int Id { get; set; }
        [Required]
        public string? Text { get; set; }
        [Required]
        public string? Title { get; set; }
    }
}
