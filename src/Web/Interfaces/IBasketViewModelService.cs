using ApplicationCore.Entities;
using System.Threading.Tasks;

namespace Web.Interfaces
{
    public interface IBasketViewModelService
    {
        Task<Basket> GetBasketAsync();
        Task<Basket> AddItemToBasketAsync(int productId, int quantity=1);
        Task<int> BasketItemsCountAsync();
    }
}
