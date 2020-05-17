using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.IntegrationTests
{
    public class ProductControllerTests : DBTestBaseController
    {
        //private readonly Controller controller;
        private readonly IProductService productService;
        private readonly ProductService productService2;
        private readonly ILanguageService languageService;
        private readonly IProductRepository productRepository;
        private readonly ICart cart;
        private readonly IOrderRepository orderRepository;
        private readonly StringLocalizer<ProductService>localizer;
        private ProductViewModel product;
        private readonly ProductController productController;

        public ProductControllerTests()
        {
            //Arrange
            var p3Referential = new P3Referential(options);

            productRepository = new ProductRepository(p3Referential);
            productService = new ProductService(cart, new ProductRepository(p3Referential), orderRepository, localizer);
            
            productController = new ProductController(productService, languageService);
            product = new ProductViewModel();
            
        }

        /*[Fact]
        public void Create()//to mock
        {
            
            product.Id = 1;
            product.Name = "Test box";
            product.Description = "The best box ever.";
            product.Details = "Toss it and see if it gets back.";
            product.Stock = "9000";
            product.Price = "9000";
            var saveProduct = productController.Create(product);
            var expectedProduct = productService2.GetProduct(1);


            Assert.NotNull(expectedProduct);
           

        }
        */

    }
}
