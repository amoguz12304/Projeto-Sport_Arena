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
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            var usuarioInserido = await usuarioService.PostUsuario(usuario);

            return CreatedAtAction("GetusuarioById", new { id = usuario.Id }, ParaResponse(usuarioInserido));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Putusuario(Guid id, Usuario usuario)
        {
            var usuarioAtualizado = await usuarioService.PutUsuario(id, usuario);
            return usuarioAtualizado is not null ? NoContent() : NotFound();
        }


        //[HttpPut("{id}/Desativar-conta")] 
        //public async Task<ActionResult> DesativaUsuario(Guid id, Usuario usuario)
        //{
        //  var usuarioDesativado =  await usuarioService.DeativarUsuario()
        //}



        [HttpDelete("{id}")]
        public async Task<ActionResult> Deleteusuario(Guid id)
        {
            var deletado = await usuarioService.DeleteUsuario(id);
            return deletado ? NoContent() : NotFound();
        }

    }
}
