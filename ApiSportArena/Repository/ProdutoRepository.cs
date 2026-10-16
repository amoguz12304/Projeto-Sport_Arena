using ApiSportArena.Data;
using ApiSportArena.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSportArena.Repository
{
    public class ProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetProduto()
        {
            return await _context.Produtos
                .Include(p => p.Categoria)
                .ToListAsync();
        }

        public async Task<bool> ProdutoDisponivel(string nome, bool disponivel)
        {
            return await _context.Produtos
                .AnyAsync(p => p.Nome == nome && p.Disponivel == disponivel );
        }

        public async Task<bool> ProdutoExiste(string nome, string modelo)
        {
            return await _context.Produtos
                .AnyAsync(p => p.Nome == nome &&  p.Modelo == modelo);
        }

        public async Task<Produto?> GetProdutoById(Guid id)
        {
            return await _context.Produtos
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Produto> Add(Produto produto)
        {
            _context.Produtos.Add(produto);

            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<bool> Update(Produto produto)
        {
            _context.Produtos.Update(produto);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return false;
            }

            _context.Produtos.Remove(produto);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
