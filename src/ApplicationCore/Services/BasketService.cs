using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class BasketService : IBasketService
    {
        private readonly IRepository<Basket> _basketRepository;

        public BasketService(IRepository<Basket> basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task DeleteBasketAsync(string buyerId)
        {
            var specBasket = new BasketSpecification(buyerId);
            var basket = await _basketRepository.FirstOrDefaultAsync(specBasket);
            await _basketRepository.DeleteAsync(basket);

        }

        public async Task EmptyBasketAsync(string buyerId)
        {
            var spec = new BasketWithItemsSpecification(buyerId);
            var basket = await _basketRepository.FirstOrDefaultAsync(spec);
            if (basket == null || basket.Items.Count == 0) return;
            basket.Items.Clear();
            await _basketRepository.UpdateAsync(basket);

        }

        public async Task RemoveBasketItemAsync(string buyerId, int basketItemId)
        {
            var spec = new BasketWithItemsSpecification(buyerId);
            var basket = await _basketRepository.FirstOrDefaultAsync(spec);
            if (basket == null || basket.Items.Count == 0) return;
            var basketItem = basket.Items.FirstOrDefault(x => x.Id == basketItemId);
            if (basketItem == null) return;
            basket.Items.Remove(basketItem);
            await _basketRepository.UpdateAsync(basket);
        }

        public async Task SetQuantitiesAsync(string buyerId, int[] basketItemIds, int[] quantities)
        {
            var spec = new BasketWithItemsSpecification(buyerId);
            var basket = await _basketRepository.FirstOrDefaultAsync(spec);
            if (basket == null || basket.Items.Count == 0) return;
            for (int i = 0; i < basketItemIds.Length; i++)
            {
                basket.Items.FirstOrDefault(x => x.Id == basketItemIds[i]).Quantity = quantities[i];
            }
            await _basketRepository.UpdateAsync(basket);



        }

        public async Task TransferBasketAsync(string anonymousId, string userId)
        {
            var specAnonymous = new BasketWithItemsSpecification(anonymousId);
            var basketAnonymous = await _basketRepository.FirstOrDefaultAsync(specAnonymous);
            var specUser = new BasketWithItemsSpecification(userId);
            var basketUser = await _basketRepository.FirstOrDefaultAsync(specUser);
            if (basketAnonymous == null) return;

            if (basketUser == null)
            {
                basketUser = new Basket() { BuyerId = userId };
                await _basketRepository.AddAsync(basketUser);
            }

            foreach (var item in basketAnonymous.Items)
            {
                var existingItem = basketUser.Items.FirstOrDefault(x => x.ProductId == item.ProductId);
                if (existingItem != null)
                {
                    existingItem.Quantity += item.Quantity;
                }
                else
                {
                    basketUser.Items.Add(new BasketItem() { ProductId = item.ProductId, Quantity = item.Quantity });
                }
            }
            await _basketRepository.UpdateAsync(basketUser);
            await _basketRepository.DeleteAsync(basketAnonymous);



        }
    }
}
