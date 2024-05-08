using ApiBiblioteca.Constants;
using ApiBiblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiBiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration? _configuration;

        public LoginController(IConfiguration? configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login(LoginUser userLogin)
        {
            var user = Autenticar(userLogin);

            if (user != null)
            {
                // Crear token
                var token = GenerarToken(user);

                return Ok("Usuario logeado exitosamente, el token generado es:" + token);
            }
            return NotFound("Usuario no encontrado");
        }

        private UserModel? Autenticar(LoginUser? userLogin)
        {
            var usuarioActual = UserConstants.Usuarios.FirstOrDefault(u => u.UserName?.ToLower() == userLogin?.UserName?.ToLower() && u.Password == userLogin?.Password);

            if (usuarioActual != null)
            {
                return usuarioActual;
            }
            return null;
        }

        private string? GenerarToken(UserModel usuario)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, usuario?.UserName ?? ""));
            claims.Add(new Claim(ClaimTypes.Email, usuario?.EmailAdress ?? ""));
            claims.Add(new Claim(ClaimTypes.GivenName, usuario?.Name ?? ""));
            claims.Add(new Claim(ClaimTypes.Surname, usuario?.LastName ?? ""));
            claims.Add(new Claim(ClaimTypes.Role, usuario?.Rol ?? ""));

            var token = new JwtSecurityToken(
                _configuration?["Jwt:Issuer"],
                _configuration?["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
