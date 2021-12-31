using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Interfaces;

namespace Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketViewModelService _basketViewModelService;
        public BasketController(IBasketViewModelService basketViewModelService)
        {
            _basketViewModelService = basketViewModelService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _basketViewModelService.GetBasketAsync());
        }


        [HttpPost]
        public async Task<IActionResult> AddItem(int productId)
        {
            var basket = await _basketViewModelService.AddItemToBasketAsync(productId);
            return Json(basket.Items.Count);
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> EmptyBasket()
        {
            await _basketViewModelService.EmptyBasketAsync();
            TempData["Message"] = "Your basket is empty.";
            return RedirectToAction("Index", "Basket");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveItem(int basketItemId)
        {
            await _basketViewModelService.RemoveBasketItemAsync(basketItemId);
            TempData["Message"] = "Item has been removed from basket.";
            return RedirectToAction("Index", "Basket");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateItems(int[] basketItemIds, int[] quantities)
        {
            await _basketViewModelService.UpdateBasketItemsAsync(basketItemIds,quantities);
            TempData["Message"] = "Items have been updated.";
            

            return RedirectToAction("Index", "Basket");
        }
    }
}
