using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string userName { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "mete entre 4 y 8")]
        public string password { get; set; }
    }
}