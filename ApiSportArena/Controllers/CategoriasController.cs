using ApiSportArena.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiSportArena.Services;
using ApiSportArena.Data;

namespace ApiSportArena.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController(CategoriasService categoriasService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            return Ok(await categoriasService.GetCategorias());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoriasById(Guid id)
        {
            var categoria = await categoriasService.GetCategoriasById(id);
            return categoria is not null ? Ok(categoria) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategorias(Categoria  categorias)
        {
            var categoriasInserido = await categoriasService.PostCategorias(categorias);

            return CreatedAtAction("GetCategoriasById", new { id = categorias.Id }, categoriasInserido);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutCategorias(Guid id, Categoria categorias)
        {
            var categoriasAtualizado = await categoriasService.PutCategorias(id, categorias);
            return categoriasAtualizado is not null ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategorias(Guid id)
        {
            var deletado = await categoriasService.DeleteCategorias(id);
            return deletado ? NoContent() : NotFound();
        }

    }
}
