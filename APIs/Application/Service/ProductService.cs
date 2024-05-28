using Application.InterfaceService;
using Application.Util;
using Application.ViewModel.CartModel;
using Application.ViewModel.ProductModel;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
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
        private readonly IMapper _mapper;
        private readonly IUploadFile _uploadFile;
        private readonly IClaimService _claimService;
        private readonly ICacheService _cacheService;
        public ProductService(IUnitOfWork unitOfWork,IMapper mapper,IUploadFile uploadFile,IClaimService claimService,ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadFile = uploadFile;
            _claimService = claimService;
            _cacheService = cacheService;
        }

        public async Task<bool> AddToCart(List<Guid> listProductId)
        {
            bool isAdded = false;
           
            string key = _claimService.GetCurrentUserId.ToString() + "_" + "Cart";
            var cartData= _cacheService.GetData<Cart>(key);
            if (cartData == null)
            {
                Cart cart = new Cart();
                foreach (var productId in listProductId)
                {
                    var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
                    var item = _mapper.Map<Item>(product);
                     cart.AddToCart(item);
                    _cacheService.SetData<Cart>(key, cart,DateTimeOffset.UtcNow.AddDays(5));
                     isAdded = true;
                }
                return isAdded; 
            }
            foreach (var productId in listProductId)
            {
                var foundInCart = false;
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
                var item = _mapper.Map<Item>(product);
                foundInCart = cartData.Items.Any(x => x.ItemId == item.ItemId);
                if (!foundInCart)
                {
                    cartData.AddToCart(item);
                    _cacheService.UpdateData(key, cartData);
                    isAdded = true;
                }
                else
                {
                    cartData.Items.Where(x => x.ItemId == item.ItemId).Select(x => { x.Amount++; return x.Amount;}).ToList();
                    _cacheService.UpdateData(key, cartData);
                    isAdded = true;
                }
            }
          return isAdded;
        }

        public async Task<bool> CreateProduct(CreateProductModel product)
        {
            var imageUrl = await _uploadFile.UploadFileToFireBase(product.ProductImage);
            var newProduct=_mapper.Map<Product>(product);
            newProduct.ProductImageUrl = imageUrl;
            await _unitOfWork.ProductRepository.AddAsync(newProduct);
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

        public bool RemoveFromCart(Guid itemId)
        {
            bool isRemoved=false;
            string key = _claimService.GetCurrentUserId.ToString() + "_" + "Cart";
            var cartData = _cacheService.GetData<Cart>(key);
            if (cartData!=null)
            {
                cartData.RemoveItemFromCart(itemId);
                _cacheService.UpdateData(key, cartData);
                isRemoved = true;
            }
            return isRemoved;   
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            _unitOfWork.ProductRepository.Update(product);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public Cart ViewCart()
        {
            string key= _claimService.GetCurrentUserId.ToString()+"_"+"Cart";
            var cart= _cacheService.GetData<Cart>(key);
            return cart;
        }
    }
}
