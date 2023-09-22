using System.ComponentModel.DataAnnotations;

namespace WebAppApi.Requests
{
    public class PostRequest
    {
        [Required]
        public string Text { get; set; }
    }
}
