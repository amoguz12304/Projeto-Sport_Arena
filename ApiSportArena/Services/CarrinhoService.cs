using ApiSportArena.Models;
using ApiSportArena.Repository;

namespace ApiSportArena.Services
{
    public class CarrinhoService(CarrinhoRepository repository)
    {
        public async Task<IEnumerable<Carrinho>> GetCarrinho()
        {
            return await repository.GetCarrinho();
        }

        public async Task<Carrinho?> GetCarrinhoById(Guid id)
        {
            return await repository.GetCarrinhoById(id);
        }

        public async Task<Carrinho> PostCarrinho(Carrinho carrinho)
        {
            

            return await repository.Add(carrinho);
        }

        public async Task<Carrinho?> Putcarrinho(Guid id, Carrinho carrinho)
        {
            var carrinhoExistente = await repository.GetCarrinhoById(id);

            if (carrinhoExistente == null)
            {
                return null;
            }

            carrinhoExistente.Id = carrinho.Id;


            await repository.Update(carrinhoExistente);

            return carrinhoExistente;
        }

        public async Task<bool> DeleteCarrinho(Guid id)
        {
            return await repository.Delete(id);
        }

    }
}
