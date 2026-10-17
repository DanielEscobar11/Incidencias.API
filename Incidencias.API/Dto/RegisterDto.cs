using System.ComponentModel.DataAnnotations;

namespace Incidencias.API.Dto
{
    public class RegisterUserDto
    {
        [Required]
        public string Username { get; set; }

        [Required, MinLength(8)]
        public string Password { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

    }
}
