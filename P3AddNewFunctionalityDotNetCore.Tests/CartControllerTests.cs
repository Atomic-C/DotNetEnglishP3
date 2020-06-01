using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Moq;
using P3AddNewFunctionalityDotNetCore.Controllers;
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
    public class CartControllerTests
    {
        private ProductViewModel product;
        private Mock<ICart> cartMock;
        private Mock<IProductRepository> productRepositoryMock;
        private Mock<IOrderRepository> orderRepositoryMock;
        private Mock<IProductService> productServiceMock;
        private Mock<ILanguageService> languageServiceMock;
        private Mock<IStringLocalizer<ProductService>> stringLocalizerMock;

        private ProductController productController;

        public CartControllerTests()
        {
            //setup
            product = new ProductViewModel();
            cartMock = new Mock<ICart>();
            productRepositoryMock = new Mock<IProductRepository>();
            orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock = new Mock<IOrderRepository>();
            productServiceMock = new Mock<IProductService>();
            languageServiceMock = new Mock<ILanguageService>();
            stringLocalizerMock = new Mock<IStringLocalizer<ProductService>>();

        }

        [Fact]
        public void DoesAddToCartWork() //MOCKED?
        {
            //Act
            ProductService productService = new ProductService(cartMock.Object, productRepositoryMock.Object, orderRepositoryMock.Object, stringLocalizerMock.Object);

            CartController cartController = new CartController(cartMock.Object, productService);
            

            //ProductController productController = new ProductController(productServiceMock.Object, languageServiceMock.Object);

            //Arrange
            var addToCart = cartController.AddToCart(3); //now removing it - //this is not working, product is null.
            //Assert
            Assert.IsType<RedirectToActionResult>(addToCart);
        }

        [Fact]
        public void DoesRemoveFromCartWork() //MOCKED?
        {
            //Act
            ProductService productService = new ProductService(cartMock.Object, productRepositoryMock.Object, orderRepositoryMock.Object, stringLocalizerMock.Object);

            productController = new ProductController(productService, languageServiceMock.Object);

            CartController cartController = new CartController(cartMock.Object, productService);

            product.Id = 99; //the idea is to populate product
            product.Name = "Test box";
            product.Description = "The best box ever.";
            product.Details = "Toss it and see if it gets back.";
            product.Stock = "9000";
            product.Price = "9000";

            var saveProduct = productController.Create(product); //then create\save

            //ProductController productController = new ProductController(productServiceMock.Object, languageServiceMock.Object);

            //Arrange
            var addToCart = cartController.AddToCart(99); //Then add populated, saved product to cart

            var removeFromCart = cartController.RemoveFromCart(99); //now removing it - //this is not working same as above, product is null.
            //Assert
            Assert.IsType<RedirectToActionResult>(removeFromCart);
        }

    }
}
