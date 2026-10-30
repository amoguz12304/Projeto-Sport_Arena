using System.Security.Claims;
using ApiSportArena.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace ApiSportArena.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(SymmetricSecurityKey chave, UsuarioService usuarioService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest dados)
        {
            var usuario = await usuarioService.ValidarLogin(dados.Email, dados.Senha);
            if (usuario is null)
                return Unauthorized(new { erro = "E-mail ou senha inválidos" });

            var expiracao = DateTime.UtcNow.AddMinutes(30);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.Funcao)
                }),
                Issuer = "ApiSportArena",
                Audience = "SportArena.Mvc",
                Expires = expiracao,
                SigningCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256)
            };

            return Ok(new
            {
                token = new JsonWebTokenHandler().CreateToken(descriptor),
                tipo = "Bearer",
                expiraEm = expiracao
            });
        }
    }

    public record LoginRequest(string Email, string Senha);
}