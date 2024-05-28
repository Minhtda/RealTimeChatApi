using Application.ViewModel.CartModel;
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
        Task<bool> CreateProduct(CreateProductModel product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(Guid productId);
        Task<bool> AddToCart(List<Guid> listProductId);
        Cart ViewCart();
        bool RemoveFromCart(Guid itemId);
        Task<List<Product>> GetAllProducts();   
    }
}
