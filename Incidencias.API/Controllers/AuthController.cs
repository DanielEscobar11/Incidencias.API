using Incidencias.API.Dto;
using Incidencias.API.Models;
using Incidencias.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Incidencias.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly TokenServices _tokenServices;
        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, TokenServices tokenServices)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenServices = tokenServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null) {
                return Unauthorized("Usuario o contraseña incorrectos");
            }

            var resultado = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!resultado)
            {
                return Unauthorized("Usuario o contraseña incorrectos");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = _tokenServices.GenerarToken(user, roles.ToList());

            return Ok(new 
            { 
                token = token,
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            // Lógica para registrar un nuevo usuario
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = dto.Username,
                Email = dto.Email
            };

            var resultado = await _userManager.CreateAsync(applicationUser, dto.Password);

            if(!resultado.Succeeded) return BadRequest(resultado.Errors);

            return Ok( new { messages= "Usuario Creado correctamente" });
        }

    }
}
