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
        public async Task EmptyBasketAsync(string buyerId)
        {
            var spec = new BasketWithItemsSpecification(buyerId);
            var basket =await _basketRepository.FirstOrDefaultAsync(spec);
            if (basket == null || basket.Items.Count == 0) return;
            basket.Items.Clear();
            await _basketRepository.UpdateAsync(basket);

        }
    }
}
