using ApiSportArena.Models;
using ApiSportArena.Repository;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;
namespace ApiSportArena.Services
{
    public class UsuarioService(UsuarioRepository repository, IDataProtectionProvider provider)
    {
        private readonly PasswordHasher<Usuario> hasher = new();
        private readonly IDataProtector protector = provider.CreateProtector("Usuario.Cpf");
        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            

            return await repository.GetUsuario();
        }

        public async Task<Usuario?> GetUsuarioById(Guid id)
        {
            return await repository.GetUsuarioById(id);
        }

        public async Task<Resultado<Usuario>> PostUsuario(Usuario usuario)
        {
            usuario.Funcao = Funcoes.Cliente;
            usuario.Status = true;

            if (!CpfValidator.IsValid(usuario.Cpf))
                return Resultado<Usuario>.Falha("CPF inválido.");

            usuario.Cpf = CpfValidator.Normalizar(usuario.Cpf);

            if (await repository.EmailExiste(usuario.Email))
                return Resultado<Usuario>.Falha("Já existe um usuário cadastrado com este E-mail.");

            if (await repository.CpfExiste(usuario.Cpf))
                return Resultado<Usuario>.Falha("Já existe um usuário cadastrado com este CPF.");

             usuario.Senha = hasher.HashPassword(usuario, usuario.Senha);
            var inserido = await repository.Add(usuario);
            return Resultado<Usuario>.Ok(inserido);
        }

        public async Task<Usuario?> ValidarLogin(string email, string senha)
        {
            var usuario = await repository.GetUsuarioByEmail(email);
            if (usuario is null || !usuario.Status) return null;

            var resultado = hasher.VerifyHashedPassword(usuario, usuario.Senha, senha);
            return resultado == PasswordVerificationResult.Failed ? null : usuario;
        }


        public async Task<Resultado<Usuario>> PutUsuario(Guid id, Usuario usuario)
        {
            var usuarioExistente = await repository.GetUsuarioById(id);
            if (usuarioExistente == null) return Resultado<Usuario>.Ok(null); // não encontrado

            var outro = await repository.GetUsuarioByEmail(usuario.Email);
            if (outro is not null && outro.Id != id)
                return Resultado<Usuario>.Falha("Este E-mail já pertence a outro usuário.");

            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Idade = usuario.Idade;
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.Foto = usuario.Foto;
            // Cpf NÃO é alterado

            await repository.Update(usuarioExistente);
            return Resultado<Usuario>.Ok(usuarioExistente);
        }

        public async Task<Resultado<bool>> AlterarSenha(Guid id, AlterarSenhaRequest request)
        {
            var usuario = await repository.GetUsuarioById(id);
            if (usuario is null) return Resultado<bool>.Ok(false); // não encontrado

            var senhaAtualOk = hasher.VerifyHashedPassword(usuario, usuario.Senha, request.SenhaAtual);
            if (senhaAtualOk == PasswordVerificationResult.Failed)
                return Resultado<bool>.Falha("Senha atual incorreta.");

            if (request.SenhaAtual == request.NovaSenha)
                return Resultado<bool>.Falha("A nova senha não pode ser igual à anterior.");

            usuario.Senha = hasher.HashPassword(usuario, request.NovaSenha);
            await repository.Update(usuario);
            return Resultado<bool>.Ok(true);
        }

        public async Task<bool> DeleteUsuario(Guid id)
        {
            return await repository.Delete(id);
        }
    }
}