using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.ViewModels
{
    public class UserLoginViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
