using ApiSportArena.Data;
using ApiSportArena.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSportArena.Repository
{
    public class CarrinhoRepository(AppDbContext context)
    {
        public async Task<IEnumerable<Carrinho>> GetCarrinho()
        {
            return await context.Carrinho
                 .Include(c => c.Itens)
                .ToListAsync();
        }

       
        public async Task<Carrinho?> GetCarrinhoById(Guid id)
        {
            return await context.Carrinho
                 .Include(c => c.Itens)
                .FirstOrDefaultAsync(u => u.Id == id);
        }


        public async Task<Carrinho> Add(Carrinho carrinho)
        {
            context.Carrinho.Add(carrinho);

            await context.SaveChangesAsync();

            return carrinho;
        }

        public async Task<bool> Update(Carrinho carrinho)
        {
            context.Carrinho.Update(carrinho);

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var carrinho = await context.Carrinho.FindAsync(id);

            if (carrinho == null)
            {
                return false;
            }

            context.Carrinho.Remove(carrinho);

            await context.SaveChangesAsync();

            return true;
        }
    }
}
