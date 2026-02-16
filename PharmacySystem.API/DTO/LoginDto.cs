using System.ComponentModel.DataAnnotations;

namespace PharmacySystem.API.DTO
{
    public class LoginDto
    {
        [Required(ErrorMessage ="Email Is Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }
    }
}
