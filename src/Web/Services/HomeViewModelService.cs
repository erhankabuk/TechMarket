using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.Models;

namespace Web.Services
{
    public class HomeViewModelService : IHomeViewModelService
    {
        private readonly IRepository<Product> _productRepository;

        public HomeViewModelService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<HomeViewModel> GetHomeViewModelAsync()
        {
            var list = (await _productRepository.ListAllAsync()).Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                PictureUri = x.PictureUri,
                Price = x.Price
            }).ToList();
            var vm = new HomeViewModel()
            {
                Products = list
            };
            return vm;

        }
    }
}
