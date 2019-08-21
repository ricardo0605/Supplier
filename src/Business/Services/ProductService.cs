using Business.Interfaces;
using Business.Models;
using Business.Models.Validations;
using System;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository,
                              INotificator notificator) : base(notificator)
        {
            _productRepository = productRepository;
        }

        public async Task AddAsync(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product))
                return;

            await _productRepository.AddAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product))
                return;

            await _productRepository.UpdateAsync(product);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _productRepository.RemoveAsync(id);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}