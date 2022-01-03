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
using Web.Models;

namespace Web.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<Basket> _basketRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        private string UserId => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        private string AnonymousId => _httpContextAccessor.HttpContext.Request.Cookies[Constants.BASKET_COOKIENAME];


        public BasketViewModelService(IHttpContextAccessor httpContextAccessor, IRepository<Basket> basketRepository, IRepository<Product> productRepository, IBasketService basketService, IOrderService orderService)
        {
            _httpContextAccessor = httpContextAccessor;
            _basketRepository = basketRepository;
            _productRepository = productRepository;
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<BasketViewModel> AddItemToBasketAsync(int productId, int quantity = 1)
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

            return ToBasketViewModel(basket);
        }
        public async Task<int> BasketItemsCountAsync()
        {
            string buyerId = UserId ?? AnonymousId;
            if (buyerId == null) return 0;
            var spec = new BasketWithItemsSpecification(buyerId);
            var basket = await _basketRepository.FirstOrDefaultAsync(spec);
            if (basket == null) return 0;
            return basket.Items.Count;

        }

        public async Task<BasketViewModel> GetBasketAsync()
        {
            string buyerId = GetOrCreateBuyerId();
            Basket basket = await GetOrCreateBasketAsync(buyerId);
            return ToBasketViewModel(basket);

        }

        private async Task<Basket> GetOrCreateBasketAsync(string buyerId)
        {
            var spec = new BasketWithItemsSpecification(buyerId);
            var basket = await _basketRepository.FirstOrDefaultAsync(spec);
            if (basket == null)
            {
                basket = new Basket() { BuyerId = buyerId };
                await _basketRepository.AddAsync(basket);
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
       private BasketViewModel ToBasketViewModel(Basket basket)
        {
            var vm = new BasketViewModel() {

                TotalPrice = basket.Items.Sum(x => x.Quantity * x.Product.Price),
                Items = basket.Items.Select(x => new BasketItemViewModel() { 
                Id=x.Id,
                ProductId=x.Product.Id,
                Price=x.Product.Price,
                PictureUri=x.Product.PictureUri,
                ProductName= x.Product.Name,
                Quantity=x.Quantity
                }).ToList()
            };

            return vm;
        }

        public async Task EmptyBasketAsync()
        {
            var buyerId = UserId ?? AnonymousId;
            if (buyerId == null) return;
            await _basketService.EmptyBasketAsync(buyerId);

        }

        public async Task RemoveBasketItemAsync(int basketItemId)
        {
            var buyerId = UserId ?? AnonymousId;
            if (buyerId == null) return;
            await _basketService.RemoveBasketItemAsync(buyerId, basketItemId);
        }

        public async Task UpdateBasketItemsAsync(int[] basketItemIds, int[] quantities)
        {
            string buyerId = UserId ?? AnonymousId;
            if (buyerId == null || basketItemIds.Length == 0) return;
            if (basketItemIds.Length != quantities.Length)
                throw new ArgumentException("The basket item ids and quantities don't match.");

            await _basketService.SetQuantitiesAsync(buyerId, basketItemIds, quantities);
        }

        public async Task<Order> CreateOrderAsync(Address address)
        {
           Order order= await _orderService.CreateOrderAsync(UserId, address);
            await _basketService.DeleteBasketAsync(UserId);
            return order;
        }
    }
}

















