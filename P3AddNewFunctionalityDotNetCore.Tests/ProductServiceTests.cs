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
        private Mock<ICart> cartMock;
        private Mock<IProductRepository> productRepositoryMock;
        private Mock<IOrderRepository> orderRepositoryMock;
        private Mock<IStringLocalizer<ProductService>> stringLocalizerMock;
        private ProductViewModel product;

        public ProductServiceTests() //A constructor is always named after the class
        {
            // Arrange
            product = new ProductViewModel();
            cartMock = new Mock<ICart>();
            productRepositoryMock = new Mock<IProductRepository>();
            orderRepositoryMock = new Mock<IOrderRepository>();
            stringLocalizerMock = new Mock<IStringLocalizer<ProductService>>();


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
            stringLocalizerMock.Setup(l => l["MissingName"]).Returns(new LocalizedString("MissingName", "MissingName"));

            ProductService productService = new ProductService(cartMock.Object, productRepositoryMock.Object, orderRepositoryMock.Object, stringLocalizerMock.Object);
            

            // Act
            List<string> modelErrors = productService.CheckProductModelErrors(product);

            // Assert
            Assert.Contains("MissingName", modelErrors); //contains missing name in model errors
        }

        [Fact]  
        public void ValidatePrice()
        {
            //Arrange
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
             product.Price = "-1"; //setting Price to -1
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
             product.Stock = "-1";
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
