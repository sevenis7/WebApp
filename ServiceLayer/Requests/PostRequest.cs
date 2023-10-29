using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Requests
{
    public class PostRequest
    {
        [Required(ErrorMessage = "Please, type your message.")]
        public string? Text { get; set; }
    }
}
