using ApiSportArena.Data;
using ApiSportArena.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSportArena.Repository
{
    public class CategoriasRepository(AppDbContext context)
    {
        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            return await context.Categorias
                .Include(c => c.Produtos)
                .ToListAsync();
        }

        public async Task<bool> NomeExiste(string nome)
        {
            return await context.Categorias
                .AnyAsync(c => c.Nome == nome);
        }
        public async Task<Categoria?> GetCategoriasById(Guid id)
        {
            return await context.Categorias
                .Include(c => c.Produtos)
                .FirstOrDefaultAsync(u => u.Id == id);
        }


        public async Task<Categoria> Add(Categoria categorias)
        {
            context.Categorias.Add(categorias);

            await context.SaveChangesAsync();

            return categorias;
        }

        public async Task<bool> Update(Categoria categorias)
        {
            context.Categorias.Update(categorias);

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var categorias = await context.Categorias.FindAsync(id);

            if (categorias == null)
            {
                return false;
            }

            context.Categorias.Remove(categorias);

            await context.SaveChangesAsync();

            return true;
        }
    }
}
