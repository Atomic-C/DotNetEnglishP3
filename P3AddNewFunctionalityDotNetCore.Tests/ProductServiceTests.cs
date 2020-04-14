using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System.Collections.Generic;
using Xunit;
using Moq;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using Microsoft.Extensions.Localization;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceTests
    {
        
        
        /// <summary>
        /// Take this test method as a template to write your test method.
        /// A test method must check if a definite method does its job:
        /// returns an expected value from a particular set of parameters
        /// </summary>
        [Fact]
        public void validateName()
        {
            // Arrange
            ProductViewModel product = new ProductViewModel();
            var cartMock = new Mock<ICart>();
            var productRepositoryMock = new Mock<IProductRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var stringLocalizerMock = new Mock<IStringLocalizer<ProductService>>();
            
            stringLocalizerMock.Setup(l => l["MissingName"]).Returns(new LocalizedString("MissingName", "MissingName"));
            //read more about lambda expressions
            //c# expressions read more into it
            ProductService productService = new ProductService(cartMock.Object, productRepositoryMock.Object, orderRepositoryMock.Object, stringLocalizerMock.Object);
            product.Name = "";

            // Act
            List<string> modelErrors = productService.CheckProductModelErrors(product);
            // Assert

            Assert.Contains("MissingName", modelErrors); //contains missing name in model errors
        }

        // TODO write test methods to ensure a correct coverage of all possibilities
    }
}