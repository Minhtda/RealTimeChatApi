using Application.InterfaceService;
using Application.Service;
using Application.ViewModel.ProductModel;
using AutoFixture;
using Backend.Domain.Test;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Application.Test.ServiceTest
{
    public class ProductServiceTest:SetupTest
    {
        private IProductService _productService;
        public ProductServiceTest()
        {
            _productService = new ProductService(_unitOfWorkMock.Object,_mapper,_uploadFileMock.Object,_claimServiceMock.Object,_cacheServiceMock.Object);
        }
      /*  [Fact]
        public async Task CreateProduct_ShouldReturnTrue()
        {
            //Arrange
            var bytes = Encoding.UTF8.GetBytes("This is a dummy file");
            _fixture.Customize<IFormFile>(x=>x.FromFactory(()=>new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.txt")));
            var product=_fixture.Build<Product>().With(x=>x.ProductTypeId,2).Create();
            var createProductModel=_fixture.Build<CreateProductModel>()
                .With(x=>x.ProductTypeId,product.ProductTypeId)
                .Create();
            //Act
            _unitOfWorkMock.Setup(unit => unit.ProductRepository.GetByIdAsync(product.Id)).ReturnsAsync(product);
            _unitOfWorkMock.Setup(unit=>unit.ProductRepository.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
            var result= await _productService.CreateProduct(createProductModel);
            //Assert
            Assert.True(result);
        }*/
    }
}
