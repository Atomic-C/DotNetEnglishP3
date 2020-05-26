using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Moq;
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

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductControllerTests
    {
        private ProductViewModel product;
        private Mock<ICart> cartMock;
        private Mock<IProductRepository> productRepositoryMock;
        private Mock<IOrderRepository> orderRepositoryMock;
        private Mock<IProductService> productServiceMock;
        private Mock<ILanguageService> languageServiceMock;
        private Mock<IStringLocalizer<ProductService>> stringLocalizerMock;
        
        private ProductController productController;

        public ProductControllerTests()
        {
            //setup
            product = new ProductViewModel();
            cartMock = new Mock<ICart>();
            productRepositoryMock = new Mock<IProductRepository>();
            orderRepositoryMock = new Mock<IOrderRepository>();
            productServiceMock = new Mock<IProductService>();
            languageServiceMock = new Mock<ILanguageService>();
            stringLocalizerMock = new Mock<IStringLocalizer<ProductService>>();
            //productController = new ProductController(productServiceMock.Object, languageServiceMock.Object);

        }

        [Fact]
        public void CreateValidModelState() // MOCKED
        {
            // Arrange
            ProductService productService = new ProductService(cartMock.Object, productRepositoryMock.Object, orderRepositoryMock.Object, stringLocalizerMock.Object);

            productController = new ProductController(productService, languageServiceMock.Object);

            productServiceMock.Setup(x => x.SaveProduct(product)); //It works without this?!? what's it FOR!?

            //Act
                       

            product.Id = 99;
            product.Name = "Test box";
            product.Description = "The best box ever.";
            product.Details = "Toss it and see if it gets back.";
            product.Stock = "9000";
            product.Price = "9000";

            var saveProduct = productController.Create(product);


            //productServiceMock.Setup(x => x.GetProductById(It.IsAny<int>())).Returns(saveProduct);
            //productServiceMock.SetupGet(x => x.GetProductById(99)).Returns("Test box");

            //var expectedProduct = productServiceMock.GetProduct(1);

            //Assert
            Assert.IsType<RedirectToActionResult>(saveProduct);
            
        }

        [Fact]
        public void CreateInvalidModelState() // MOCKED
        {
            // Arranje
            stringLocalizerMock.Setup(l => l["MissingName"]).Returns(new LocalizedString("MissingName", "MissingName"));

            ProductService productService = new ProductService(cartMock.Object, productRepositoryMock.Object, orderRepositoryMock.Object, stringLocalizerMock.Object);

            productController = new ProductController(productService, languageServiceMock.Object);

            productServiceMock.Setup(x => x.SaveProduct(product)); //It works without this?!? what's it FOR!?

            //Act


            product.Id = 99;
            //product.Name = "Test box"; //commenting this line should cause the error test to pass.
            product.Description = "The best box ever.";
            product.Details = "Toss it and see if it gets back.";
            product.Stock = "9000";
            product.Price = "9000";

            var saveProduct = productController.Create(product);


            //productServiceMock.Setup(x => x.GetProductById(It.IsAny<int>())).Returns(saveProduct);
            //productServiceMock.SetupGet(x => x.GetProductById(99)).Returns("Test box");

            //var expectedProduct = productServiceMock.GetProduct(1);

            //Assert
            Assert.IsType<ViewResult>(saveProduct);

        }

        [Fact]
        public void IsProductDeleted() // MOCKED?
        {
            // Arranje
            //stringLocalizerMock.Setup(l => l["MissingName"]).Returns(new LocalizedString("MissingName", "MissingName"));

            ProductService productService = new ProductService(cartMock.Object, productRepositoryMock.Object, orderRepositoryMock.Object, stringLocalizerMock.Object);

            productController = new ProductController(productService, languageServiceMock.Object);

            productServiceMock.Setup(x => x.SaveProduct(product)); //ask John what this does exactly, it also passes with this comented?!?!?

            //Act




            product.Id = 99;
            product.Name = "Test box"; 
            product.Description = "The best box ever.";
            product.Details = "Toss it and see if it gets back.";
            product.Stock = "9000";
            product.Price = "9000";

            //var saveProduct = productController.Create(product);
            var deleteProduct = productController.DeleteProduct(99);

            //productServiceMock.Setup(x => x.GetProductById(It.IsAny<int>())).Returns(saveProduct);
            //productServiceMock.SetupGet(x => x.GetProductById(99)).Returns("Test box");

            //var expectedProduct = productServiceMock.GetProduct(1);

            //Assert
            Assert.IsType<RedirectToActionResult>(deleteProduct);

        }


    }
}
