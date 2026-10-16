using ApiSportArena.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiSportArena.Services;
using Microsoft.AspNetCore.Authorization;

namespace ApiSportArena.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController(ProdutoService produtoService) : ControllerBase
    {
        [Authorize(Roles = "Admin, Funcionário, Cliente")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return Ok(await produtoService.GetProdutos());
        }

        [Authorize(Roles = "Admin, Funcionário, Cliente")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProdutoById(Guid id)
        {
            var produto = await produtoService.GetProdutoById(id);
            return produto is not null ? Ok(produto) : NotFound();
        }

        [Authorize(Roles = "Admin, Funcionário")]
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            var produtoInserido = await produtoService.PostProduto(produto);

            return CreatedAtAction("GetProdutoById", new { id = produto.Id }, produtoInserido);
        }

        [Authorize(Roles = "Admin, Funcionário")]
        [HttpPut("{id}  ")]
        public async Task<ActionResult> PutProduto(Guid id, Produto produto)
        {
            var produtoAtualizado = await produtoService.PutProduto(id, produto);
            return produtoAtualizado is not null ? NoContent() : NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduto(Guid id)
        {
            var deletado = await produtoService.DeleteProduto(id);
            return deletado ? NoContent() : NotFound();
        }
    }
}
