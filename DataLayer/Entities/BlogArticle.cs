using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class BlogArticle
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? ImageBase64 { get; set; }
        public DateTime PublicationDate { get; set; }
        [Required]
        public string? Text { get; set; }
    }
}
