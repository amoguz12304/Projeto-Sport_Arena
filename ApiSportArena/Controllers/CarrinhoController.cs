using ApiSportArena.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiSportArena.Services;
using ApiSportArena.Data;

namespace ApiSportArena.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoController(CarrinhoService carrinhoService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrinho>>> Getcarrinho()
        {
            return Ok(await carrinhoService.GetCarrinho());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Carrinho>> GetcarrinhoById(Guid id)
        {
            var categoria = await carrinhoService.GetCarrinhoById(id);
            return categoria is not null ? Ok(categoria) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Carrinho>> PostCarrinho(Carrinho carrinho)
        {
            var carrinhoInserido = await carrinhoService.PostCarrinho(carrinho);

            return CreatedAtAction("GetcarrinhoById", new { id = carrinho.Id }, carrinhoInserido);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Putcarrinho(Guid id, Carrinho carrinho)
        {
            var carrinhoAtualizado = await carrinhoService.Putcarrinho(id, carrinho);
            return carrinhoAtualizado is not null ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCarrinho(Guid id)
        {
            var deletado = await carrinhoService.DeleteCarrinho(id);
            return deletado ? NoContent() : NotFound();
        }
            
    }
}
