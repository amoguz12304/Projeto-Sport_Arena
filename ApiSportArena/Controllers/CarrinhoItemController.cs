using ApiSportArena.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiSportArena.Services;
using ApiSportArena.Data;

namespace ApiSportArena.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoItemController(CarrinhoItemService CarrinhoItemService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarrinhoItem>>> GetCarrinhoItem()
        {
            return Ok(await CarrinhoItemService.GetCarrinhoItem());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarrinhoItem>> GetCarrinhoItemById(Guid id)
        {
            var categoria = await CarrinhoItemService.GetCarrinhoItemById(id);
            return categoria is not null ? Ok(categoria) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<CarrinhoItem>> PostCarrinhoItem(CarrinhoItem CarrinhoItem)
        {
            var CarrinhoItemInserido = await CarrinhoItemService.PostCarrinhoItem(CarrinhoItem);

            return CreatedAtAction("GetCarrinhoItemById", new { id = CarrinhoItem.Id }, CarrinhoItemInserido);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutCarrinhoItem(Guid id, CarrinhoItem CarrinhoItem)
        {
            var CarrinhoItemAtualizado = await CarrinhoItemService.PutCarrinhoItem(id, CarrinhoItem);
            return CarrinhoItemAtualizado is not null ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCarrinhoItem(Guid id)
        {
            var deletado = await CarrinhoItemService.DeleteCarrinhoItem(id);
            return deletado ? NoContent() : NotFound();
        }

    }
}

