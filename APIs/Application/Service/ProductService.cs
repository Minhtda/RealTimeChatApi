﻿using Application.InterfaceService;
using Application.Util;
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
        public ProductService(IUnitOfWork unitOfWork,IMapper mapper,IUploadFile uploadFile)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadFile = uploadFile;
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

        public async Task<bool> UpdateProduct(Product product)
        {
            _unitOfWork.ProductRepository.Update(product);
            return await _unitOfWork.SaveChangeAsync() > 0;
        }
    }
}
