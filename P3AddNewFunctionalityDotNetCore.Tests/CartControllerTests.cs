using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Moq;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class CartControllerTests
    {
        private Mock<ICart> cartMock;
        private Mock<IProductRepository> productRepositoryMock;
        private Mock<IOrderRepository> orderRepositoryMock;
        private Mock<IProductService> productServiceMock;
        private Mock<ILanguageService> languageServiceMock;
        private Mock<IStringLocalizer<ProductService>> stringLocalizerMock;

        public CartControllerTests()
        {
            //setup
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
            var addToCart = cartController.AddToCart(3);
            //Assert
            Assert.IsType<RedirectToActionResult>(addToCart);
        }

        [Fact]
        public void DoesRemoveFromCartWork() //MOCKED?
        {
            //Act
            ProductService productService = new ProductService(cartMock.Object, productRepositoryMock.Object, orderRepositoryMock.Object, stringLocalizerMock.Object);

            CartController cartController = new CartController(cartMock.Object, productService);

            //ProductController productController = new ProductController(productServiceMock.Object, languageServiceMock.Object);

            //Arrange
            var removeFromCart = cartController.RemoveFromCart(3);
            //Assert
            Assert.IsType<RedirectToActionResult>(removeFromCart);
        }

    }
}
