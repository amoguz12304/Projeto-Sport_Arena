using ApiSportArena.Models;
using ApiSportArena.Repository;

namespace ApiSportArena.Services
{
    public class CategoriasService(CategoriasRepository repository)
    {
        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            return await repository.GetCategorias();
        }

        public async Task<Categoria?> GetCategoriasById(Guid id)
        {
            return await repository.GetCategoriasById(id);
        }

        public async Task<Categoria> PostCategorias(Categoria categorias)
        {
            var nomeExiste = await repository.NomeExiste(categorias.Nome);

            if (nomeExiste)
            {
                throw new InvalidOperationException(
                    "Já existe uma categoria cadastrada com esse nome"
                );
            }


            return await repository.Add(categorias);
        }

        public async Task<Categoria?> PutCategorias(Guid id, Categoria   categorias)
        {
            var categoriasExistente = await repository.GetCategoriasById(id);

            if (categoriasExistente == null)
            {
                return null;
            }

            categoriasExistente.Nome = categorias.Nome;
         

            await repository.Update(categoriasExistente);

            return categoriasExistente;
        }

        public async Task<bool> DeleteCategorias(Guid id)
        {
            return await repository.Delete(id);
        }
    
     }
}
