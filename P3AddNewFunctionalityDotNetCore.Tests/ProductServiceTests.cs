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
        public ProductServiceTests()
        {

        }
        
        /// <summary>
        /// Take this test method as a template to write your test method.
        /// A test method must check if a definite method does its job:
        /// returns an expected value from a particular set of parameters
        /// </summary>
        [Fact]
        public void ValidateName()
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

        [Fact]  
        public void ValidatePrice()
        {
            //Arrange
            ProductViewModel product = new ProductViewModel();
            var cartMock = new Mock<ICart>();
            var productRepositoryMock = new Mock<IProductRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var stringLocalizerMock = new Mock<IStringLocalizer<ProductService>>();

            stringLocalizerMock.Setup(p => p["MissingPrice"]).Returns(new LocalizedString("MissingPrice", "MissingPrice"));
            ProductService productService = new ProductService(cartMock.Object, productRepositoryMock.Object, orderRepositoryMock.Object, stringLocalizerMock.Object);


            //Act
            List<string> modelErrors = productService.CheckProductModelErrors(product);
            //Assert
            Assert.Contains("MissingPrice", modelErrors);
        }

        [Fact]
        public void ValidatePriceNumber()
        {
            //Arrange
            ProductViewModel product = new ProductViewModel();
            var cartMock = new Mock<ICart>();
            var productRepositoryMock = new Mock<IProductRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var stringLocalizerMock = new Mock<IStringLocalizer<ProductService>>();

            stringLocalizerMock.Setup(p => p["PriceNotANumber"]).Returns(new LocalizedString("PriceNotANumber", "PriceNotANumber"));
            ProductService productService = new ProductService(cartMock.Object, productRepositoryMock.Object, orderRepositoryMock.Object, stringLocalizerMock.Object);


            //Act
            List<string> modelErrors = productService.CheckProductModelErrors(product);
            //Assert
            Assert.Contains("PriceNotANumber", modelErrors);
        }

        [Fact]
        public void ValidatePriceGreaterThanZero()
        {
            //Arrange
            ProductViewModel product = new ProductViewModel();
            product.Price = "-1"; //setting Price to -1

            var cartMock = new Mock<ICart>();
            var productRepositoryMock = new Mock<IProductRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var stringLocalizerMock = new Mock<IStringLocalizer<ProductService>>();

            stringLocalizerMock.Setup(p => p["PriceNotGreaterThanZero"]).Returns(new LocalizedString("PriceNotGreaterThanZero", "PriceNotGreaterThanZero"));
            ProductService productService = new ProductService(cartMock.Object, productRepositoryMock.Object, orderRepositoryMock.Object, stringLocalizerMock.Object);


            //Act
            List<string> modelErrors = productService.CheckProductModelErrors(product);



            //Assert
            Assert.Contains("PriceNotGreaterThanZero", modelErrors);
        }

        [Fact]
        public void ValidateQuantity()
        {
            //Arrange
            ProductViewModel product = new ProductViewModel();

            var cartMock = new Mock<ICart>();
            var productRepositoryMock = new Mock<IProductRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var stringLocalizerMock = new Mock<IStringLocalizer<ProductService>>();

            stringLocalizerMock.Setup(p => p["MissingQuantity"]).Returns(new LocalizedString("MissingQuantity", "MissingQuantity"));
            ProductService productService = new ProductService(cartMock.Object, productRepositoryMock.Object, orderRepositoryMock.Object, stringLocalizerMock.Object);


            //Act
            List<string> modelErrors = productService.CheckProductModelErrors(product);



            //Assert
            Assert.Contains("MissingQuantity", modelErrors);
        }

        [Fact]
        public void ValidateStockType()
        {
            //Arrange
            ProductViewModel product = new ProductViewModel();
            
            var cartMock = new Mock<ICart>();
            var productRepositoryMock = new Mock<IProductRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var stringLocalizerMock = new Mock<IStringLocalizer<ProductService>>();

            stringLocalizerMock.Setup(p => p["StockNotAnInteger"]).Returns(new LocalizedString("StockNotAnInteger", "StockNotAnInteger"));
            ProductService productService = new ProductService(cartMock.Object, productRepositoryMock.Object, orderRepositoryMock.Object, stringLocalizerMock.Object);


            //Act
            List<string> modelErrors = productService.CheckProductModelErrors(product);



            //Assert
            Assert.Contains("StockNotAnInteger", modelErrors);
        }

        [Fact]
        public void ValidateStockNotGreaterThanZero()
        {
            //Arrange
            ProductViewModel product = new ProductViewModel();
            product.Stock = "-1";
            
            var cartMock = new Mock<ICart>();
            var productRepositoryMock = new Mock<IProductRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var stringLocalizerMock = new Mock<IStringLocalizer<ProductService>>();

            stringLocalizerMock.Setup(p => p["StockNotGreaterThanZero"]).Returns(new LocalizedString("StockNotGreaterThanZero", "StockNotGreaterThanZero"));
            ProductService productService = new ProductService(cartMock.Object, productRepositoryMock.Object, orderRepositoryMock.Object, stringLocalizerMock.Object);


            //Act
            List<string> modelErrors = productService.CheckProductModelErrors(product);



            //Assert
            Assert.Contains("StockNotGreaterThanZero", modelErrors);
        }

        // TODO write test methods to ensure a correct coverage of all possibilities
    }
}

/*
    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }
        //[Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }
        //[Required]
        //[Range(0, int.MaxValue, ErrorMessage = "Please enter valid  number")]
        public string Stock { get; set; }
        //[Required]
        //[Range(0, double.MaxValue, ErrorMessage = "Please enter valid price")]
        public string Price { get; set; }
    } 
*/
