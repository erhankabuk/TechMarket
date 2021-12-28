using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Interfaces;

namespace Web.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Basket> _basketRepository;
        private readonly IRepository<Product> _productRepository;

        private string UserId => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        private string AnonymousId => _httpContextAccessor.HttpContext.Request.Cookies[Constants.BASKET_COOKIENAME];


        public BasketViewModelService(IHttpContextAccessor httpContextAccessor, IRepository<Basket> basketRepository, IRepository<Product> productRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _basketRepository = basketRepository;
            _productRepository = productRepository;
        }

        public async Task<Basket> AddItemToBasketAsync(int productId, int quantity = 1)
        {

            string buyerId = GetOrCreateBuyerId();
            Basket basket = await GetOrCreateBasketAsync(buyerId);
            BasketItem basketItem = basket.Items.FirstOrDefault(x => x.ProductId == productId);

            if (basketItem != null)
            {
                basketItem.Quantity += quantity;
            }
            else
            {
                basketItem = new BasketItem() { ProductId = productId, Quantity = quantity, Product = await _productRepository.GetByIdAsync(productId) };
                basket.Items.Add(basketItem);
            }
            await _basketRepository.UpdateAsync(basket);

            return basket;
        }

        private async Task<Basket> GetOrCreateBasketAsync(string buyerId)
        {
            var spec = new BasketWithItemsSpecification(buyerId);
            var basket = await _basketRepository.FirstOrDefaultAsync(spec);
            if (basket == null)
            {
                basket = new Basket() { BuyerId = buyerId };
                await _basketRepository.AddAsyc(basket);
            }
            return basket;
        }

        private string GetOrCreateBuyerId()
        {
            if (UserId != null) return UserId;
            if (AnonymousId != null) return AnonymousId;
            //Created cookie
            string newGuid = Guid.NewGuid().ToString();
            _httpContextAccessor.HttpContext.Response.Cookies.Append(Constants.BASKET_COOKIENAME, newGuid, new CookieOptions()
            {
                Expires = DateTime.Now.AddYears(1),
                IsEssential = true
            });
            return newGuid;

        }
        //todo:don't create new basket or buyer id if not exists
        public async Task<int> BasketItemsCountAsync()
        {
            string buyerId = GetOrCreateBuyerId();
            Basket basket = await GetOrCreateBasketAsync(buyerId);
            return basket.Items.Count;
        }

        public async Task<Basket> GetBasketAsync()
        {
            string buyerId = GetOrCreateBuyerId();
            Basket basket = await GetOrCreateBasketAsync(buyerId);
            return basket;

        }
    }
}
