using ApiSportArena.Models;
using ApiSportArena.Repository;
using Microsoft.EntityFrameworkCore;

namespace ApiSportArena.Services
{
    public class UsuarioService(UsuarioRepository repository)
    {
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
            var emailExiste = await repository.EmailExiste(usuario.Email);
 
            var CpfExiste = await repository.EmailExiste(usuario.Cpf);

            if (emailExiste)
            {
                throw new InvalidOperationException(
                    "Já existe um usuario cadastrado com este E-mail."
                );
            }
            if (CpfExiste)
            {
                throw new InvalidOperationException(
                    "CPF inválido"
                );
            }

            return await repository.Add(usuario);
        }

        public async Task<Usuario?> PutUsuario(Guid id, Usuario usuario)
        {
            var emailExiste = await repository.SenhaExiste(usuario.Senha);
            var senhaExiste = await repository.SenhaExiste(usuario.Senha);
            var usuarioExistente = await repository.GetUsuarioById(id);

            if (usuarioExistente == null)
            {
                return null;
            }


            if (emailExiste)
            {
                throw new InvalidOperationException(
                    "Ete E-mail já pertence a um usuário"
                );
            }

            if (senhaExiste)
            {
                throw new InvalidOperationException(
                    "A nova senha não pode ser igual a anterior"
                );
            }

            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Idade = usuario.Idade;
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.Senha = usuario.Senha;
            usuarioExistente.Cpf = usuario.Cpf;
            usuarioExistente.Foto = usuario.Foto;
           

            await repository.Update(usuarioExistente);

            return usuarioExistente;
        }

        public async Task<bool> DeleteUsuario(Guid id)
        {
            return await repository.Delete(id);
        }
    }
}