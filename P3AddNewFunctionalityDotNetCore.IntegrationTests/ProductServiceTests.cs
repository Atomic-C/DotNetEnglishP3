using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.IntegrationTests
{
    public class ProductServiceTests
    {
        private readonly IProductService productService;
        private readonly ICart cart;
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IStringLocalizer<ProductService>localizer;

        public ProductServiceTests() //A constructor is always named after the class
        {
            // Arrange
            var options = new DbContextOptionsBuilder<P3Referential>().UseSqlServer("Server=.\\SQLEXPRESS;Database=P3Referential-2f561d3b-493f-46fd-83c9-6e2643e7bd0a;Trusted_Connection=True;MultipleActiveResultSets=true").Options;
            var context = new P3Referential(options);
            productRepository = new ProductRepository(context);
            orderRepository = new OrderRepository(context);

            productService = new ProductService(cart, productRepository, orderRepository, localizer);
        }

        [Fact]
        public void GetAllProductsTest()
        {   //Act
            var productList = productService.GetAllProducts(); // This tests GetAllProducts() method from IProductService as we're testing methods from IproductService

            //Assert
            Assert.NotEmpty(productList);
        }
        [Fact]
        public void GetProductByIdViewModel() // This test is to see if the view model contains the correct information
        {
            //Act
            var expectedProduct = productService.GetProductByIdViewModel(18);
            //var expectedProductName = productService.GetProductByIdViewModel(18).Name;
            //var expectedPrice = productService.GetProductByIdViewModel(18).Price;

            //Assert
            Assert.Equal(18, expectedProduct.Id);
            Assert.Equal("Something", expectedProduct.Name);
            Assert.Equal("1", expectedProduct.Price); //hAVING MULT

        }
        [Fact]
        public void GetProductById()
        {
            //Act
            var expectedProduct = productService.GetProductById(18);
            //Assert
            //Assert.Equal("Something", expectedProduct.Name);
            Assert.NotNull(expectedProduct); //Assert null to see if a product is returned at all. if it exists.

        }

        [Fact]
        public void GetProduct()
        {
            //Act
            var expectedProduct = productService.GetProduct(18);
            //Assert
            Assert.NotNull(expectedProduct);
        }

        [Fact]
        public void GetProducts()
            {
            //Act
            var expectedProducts = productService.GetProducts();
            //Assert
            Assert.NotEmpty(expectedProducts.Result); //C# ASYNCRHONOUS WHAT IS, WHY USE IT
            }



    }
    
}
