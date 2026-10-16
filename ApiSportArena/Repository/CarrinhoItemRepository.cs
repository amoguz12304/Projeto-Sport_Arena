using ApiSportArena.Data;
using ApiSportArena.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSportArena.Repository
{
    public class CarrinhoItemRepository(AppDbContext context)
    {
        public async Task<IEnumerable<CarrinhoItem>> GetCarrinhoItem()
        {
            return await context.CarrinhoItem
                .Include(c => c.Produto)
                .ToListAsync();
        }


        public async Task<CarrinhoItem?> GetCarrinhoItemById(Guid id)
        {
            return await context.CarrinhoItem
                 .Include(c => c.Produto)
                .FirstOrDefaultAsync(u => u.Id == id);
        }


        public async Task<CarrinhoItem> Add(CarrinhoItem carrinhoItem)
        {
            context.CarrinhoItem.Add(carrinhoItem);

            await context.SaveChangesAsync();

            return carrinhoItem;
        }

        public async Task<bool> Update(CarrinhoItem carrinhoItem)
        {
            context.CarrinhoItem.Update(carrinhoItem);

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var carrinhoItem = await context.CarrinhoItem.FindAsync(id);

            if (carrinhoItem == null)
            {
                return false;
            }

            context.CarrinhoItem.Remove(carrinhoItem );

            await context.SaveChangesAsync();

            return true;
        }
    }
}
