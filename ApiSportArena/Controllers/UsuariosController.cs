using ApiSportArena.Data;
using ApiSportArena.Models;
using ApiSportArena.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiSportArena.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController(UsuarioService usuarioService) : ControllerBase
    {
        private static UsuarioResponse ParaResponse(Usuario u) =>
         new(u.Id, u.Nome, u.Idade, u.Email, u.Cpf, u.Status, u.Funcao, u.Foto);
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioResponse>>> GetUsuarios()
        {
            var usuarios = await usuarioService.GetUsuarios();
            return Ok(usuarios.Select(ParaResponse));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponse>> GetUsuarioById(Guid id)
        {
            var usuario = await usuarioService.GetUsuarioById(id);
            return usuario is not null ? Ok(ParaResponse(usuario)) : NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<UsuarioResponse>> PostUsuario(Usuario usuario)
        {
            var resultado = await usuarioService.PostUsuario(usuario);

            if (!resultado.Sucesso)
                return BadRequest(new { message = resultado.Erro });

            return CreatedAtAction(nameof(GetUsuarioById),
                new { id = resultado.Valor!.Id },
                ParaResponse(resultado.Valor!));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Putusuario(Guid id, Usuario usuario)
        {
            var resultado = await usuarioService.PutUsuario(id, usuario);

            if (!resultado.Sucesso)
                return BadRequest(new { message = resultado.Erro });

            return resultado.Valor is null ? NotFound() : NoContent();
        }

        [HttpPut("{id}/alterar-senha")]
        public async Task<ActionResult> AlterarSenha(Guid id, AlterarSenhaRequest request)
        {
            var resultado = await usuarioService.AlterarSenha(id, request);

            if (!resultado.Sucesso)
                return BadRequest(new { message = resultado.Erro });

            return resultado.Valor ? NoContent() : NotFound();
        }

    }
}
