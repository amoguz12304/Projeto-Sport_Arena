using ApiSportArena.Models;
using ApiSportArena.Repository;
using ApiSportArena.Data;
using Microsoft.EntityFrameworkCore;namespace ApiSportArena.Services
{
    public class ProdutoService
    {
        private readonly ProdutoRepository _repository;
       
        public ProdutoService(ProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Produto>> GetProdutos()
        {
            return await _repository.GetProduto();
        }

        public async Task<Produto?> GetProdutoById(Guid id)
        {
            return await _repository.GetProdutoById(id);
        }

        public async Task<Produto> PostProduto(Produto produto)
        {

            var produtoExiste = await _repository.ProdutoExiste(produto.Nome, produto.Modelo);

            if (produtoExiste)
            {
                throw new InvalidOperationException(
                    "Este Produto já está cadastrado"
                );
            }

            return await _repository.Add(produto);
        }

        public async Task<Produto?> PutProduto(Guid id, Produto produto)
        {
            var produtoExistente = await _repository.GetProdutoById(id);

            if (produtoExistente == null)
            {
                return null;
            }

            produtoExistente.Nome = produto.Nome;
            produtoExistente.Descricao = produto.Descricao;
            produtoExistente.Preco = produto.Preco;
            produtoExistente.CategoriaId = produto.CategoriaId;
            produtoExistente.Categoria = produto.Categoria;
            produtoExistente.Modelo = produto.Modelo;
            produtoExistente.Estoque = produto.Estoque;
            produtoExistente.Imagem = produto.Imagem;
            produtoExistente.Disponivel = produto.Disponivel;

            await _repository.Update(produtoExistente);

            return produtoExistente;
        }

        public async Task<bool> DeleteProduto(Guid id)
        {
            return await _repository.Delete(id);
        }
    }
}



