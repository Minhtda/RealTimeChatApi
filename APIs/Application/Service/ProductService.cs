using Application.InterfaceService;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateProduct(Product product)
        {
            await _unitOfWork.ProductRepository.AddAsync(product);
            return await _unitOfWork.SaveChangeAsync()>0;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            Product findProduct=await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            if (findProduct!=null)
            {
                _unitOfWork.ProductRepository.SoftRemove(findProduct);
            }
            return await _unitOfWork.SaveChangeAsync()>0;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> products=await _unitOfWork.ProductRepository.GetAllAsync();
            return products;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            _unitOfWork.ProductRepository.Update(product);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
