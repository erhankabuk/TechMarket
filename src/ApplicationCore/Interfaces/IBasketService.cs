using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task EmptyBasketAsync(string buyerId); 
        Task DeleteBasketAsync(string buyerId);
        Task RemoveBasketItemAsync(string buyerId, int basketItemId);

        Task SetQuantitiesAsync(string  buyerId, int[] basketItemIds, int[] quantities);
        Task TransferBasketAsync(string anonymousId, string userId);
    }
}
