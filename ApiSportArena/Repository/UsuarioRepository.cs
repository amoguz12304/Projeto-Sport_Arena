using ApiSportArena.Data;
using ApiSportArena.Models;
using ApiSportArena.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiSportArena.Repository
{
    public class UsuarioRepository(AppDbContext context)
    {
        public async Task<IEnumerable<Usuario>> GetUsuario()
        {
            return await context.Usuarios.ToListAsync();

        }

        public async Task<Usuario?> GetUsuarioById(Guid id)
        {
            return await context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> EmailExiste(string email)
        {
            return await context.Usuarios
                .AnyAsync(u => u.Email == email);
        }

        public async Task<Usuario?> GetUsuarioByEmail(string email)
        {
            return await context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<bool> CpfExiste(string cpf)
        {
            return await context.Usuarios
                .AnyAsync(u => u.Cpf == cpf);
        }

       

        public async Task<bool> SenhaExiste(string senha)
        {
            return await context.Usuarios
                .AnyAsync(u => u.Senha == senha);
        }

        public async Task<Usuario> Add(Usuario usuario)
        {
            context.Usuarios.Add(usuario);

            await context.SaveChangesAsync();

            return usuario;
        }

        public async Task<bool> Update(Usuario usuario)
        {
            context.Usuarios.Update(usuario);

            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var usuario = await context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return false;
            }

            context.Usuarios.Remove(usuario);

            await context.SaveChangesAsync();

            return true;
        }
    }
}