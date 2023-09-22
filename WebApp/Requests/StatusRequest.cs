using System.ComponentModel.DataAnnotations;

namespace WebAppApi.Requests
{
    public class StatusRequest
    {
        [Required]
        public string Status { get; set; }
    }
}
