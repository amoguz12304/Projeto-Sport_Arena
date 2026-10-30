using ApiSportArena.Models;
using ApiSportArena.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiSportArena.Services
{
    public class UsuarioService(UsuarioRepository repository)
    {
        private readonly PasswordHasher<Usuario> hasher = new();
        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            return await repository.GetUsuario();
        }

        public async Task<Usuario?> GetUsuarioById(Guid id)
        {
            return await repository.GetUsuarioById(id);
        }

        public async Task<Usuario> PostUsuario(Usuario usuario)
        {
            if (await repository.EmailExiste(usuario.Email))
                throw new InvalidOperationException("Já existe um usuário cadastrado com este E-mail.");

            if (await repository.CpfExiste(usuario.Cpf))          // corrigido: antes chamava EmailExiste
                throw new InvalidOperationException("Já existe um usuário cadastrado com este CPF.");

            usuario.Senha = hasher.HashPassword(usuario, usuario.Senha);
            return await repository.Add(usuario);
        }

        public async Task<Usuario?> ValidarLogin(string email, string senha)
        {
            var usuario = await repository.GetUsuarioByEmail(email);
            if (usuario is null || !usuario.Status) return null;

            var resultado = hasher.VerifyHashedPassword(usuario, usuario.Senha, senha);
            return resultado == PasswordVerificationResult.Failed ? null : usuario;
        }
        

        public async Task<Usuario?> PutUsuario(Guid id, Usuario usuario)
        {

            var usuarioExistente = await repository.GetUsuarioById(id);
            if (usuarioExistente == null) return null;

            var outro = await repository.GetUsuarioByEmail(usuario.Email);
            if (outro is not null && outro.Id != id)
                throw new InvalidOperationException("Este E-mail já pertence a outro usuário.");

            if (hasher.VerifyHashedPassword(usuarioExistente, usuarioExistente.Senha, usuario.Senha)
                    != PasswordVerificationResult.Failed)
                throw new InvalidOperationException("A nova senha não pode ser igual à anterior.");

            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Idade = usuario.Idade;
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.Cpf = usuario.Cpf;
            usuarioExistente.Foto = usuario.Foto;
            usuarioExistente.Senha = hasher.HashPassword(usuarioExistente, usuario.Senha);

            await repository.Update(usuarioExistente);
            return usuarioExistente;
        }

        public async Task<bool> DeleteUsuario(Guid id)
        {
            return await repository.Delete(id);
        }
    }
}