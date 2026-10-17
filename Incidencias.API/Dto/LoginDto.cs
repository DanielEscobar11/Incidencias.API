using System.ComponentModel.DataAnnotations;

namespace Incidencias.API.Dto
{
    public class LoginDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }
    }
}
