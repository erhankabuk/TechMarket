using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.Models;

namespace Web.Services
{
    public class HomeViewModelService : IHomeViewModelService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Brand> _brandRepository;

        public HomeViewModelService(IRepository<Product> productRepository, IRepository<Category> categoryRepository, IRepository<Brand> brandRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
        }

        public async Task<HomeViewModel> GetHomeViewModelAsync(int? categoryId, int? brandId, int page)
        {
            var specProducts = new ProductsFilterSpecification(categoryId, brandId);
            var specProductsPaginated = new ProductsFilterSpecification(categoryId, brandId, page, Constants.ITEMS_PER_PAGE);
            var totalProductsCount = await _productRepository.CountAsync(specProducts);
            var productsPaginated = await _productRepository.ListAsync(specProductsPaginated);
            var pi = new PaginationInfoViewModel(totalProductsCount, page, Constants.ITEMS_PER_PAGE, productsPaginated.Count);


            var list = productsPaginated.Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    PictureUri = x.PictureUri,
                    Price = x.Price
                }).ToList();
            var vm = new HomeViewModel()
            {
                PaginationInfo=pi,
                Products = list,
                Categories = (await _categoryRepository.ListAllAsync())
                 .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList(),
                Brands = (await _brandRepository.ListAllAsync())
                 .Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList(),
                CategoryId = categoryId,
                BrandId = brandId
            };

            return vm;

        }
    }
}
