using ApiSportArena.Models;
using ApiSportArena.Repository;

namespace ApiSportArena.Services
{
    public class CarrinhoItemService(CarrinhoItemRepository repository)
    {
        public async Task<IEnumerable<CarrinhoItem>> GetCarrinhoItem()
        {
            return await repository.GetCarrinhoItem();
        }

        public async Task<CarrinhoItem?> GetCarrinhoItemById(Guid id)
        {
            return await repository.GetCarrinhoItemById(id);
        }

        public async Task<CarrinhoItem> PostCarrinhoItem(CarrinhoItem CarrinhoItem)
        {


            return await repository.Add(CarrinhoItem);
        }

        public async Task<CarrinhoItem?> PutCarrinhoItem(Guid id, CarrinhoItem CarrinhoItem)
        {
            var ItemExistente = await repository.GetCarrinhoItemById(id);

            if (ItemExistente == null)
            {
                return null;
            }

            ItemExistente.CarrinhoId = CarrinhoItem.CarrinhoId;
            ItemExistente.ProdutoId = CarrinhoItem.ProdutoId;
            ItemExistente.Quantidade = CarrinhoItem.Quantidade;
            ItemExistente.Produto = CarrinhoItem.Produto;

            await repository.Update(ItemExistente);

            return ItemExistente;
        }

        public async Task<bool> DeleteCarrinhoItem(Guid id)
        {
            return await repository.Delete(id);
        }

    }
}
