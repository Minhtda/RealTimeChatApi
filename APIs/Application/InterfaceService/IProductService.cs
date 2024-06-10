using Application.ViewModel.ProductModel;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceService
{
    public interface IProductService
    {
        Task<bool> CreateProduct(CreatePostViewModel product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(Guid productId);
        Task<List<Product>> GetAllProducts();   
    }
}
