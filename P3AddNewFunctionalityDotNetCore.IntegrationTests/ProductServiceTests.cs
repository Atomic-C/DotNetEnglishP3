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

        public ProductServiceTests()
        {
            var options = new DbContextOptionsBuilder<P3Referential>().UseSqlServer("Server=.\\SQLEXPRESS;Database=P3Referential-2f561d3b-493f-46fd-83c9-6e2643e7bd0a;Trusted_Connection=True;MultipleActiveResultSets=true").Options;
            var context = new P3Referential(options);
            productRepository = new ProductRepository(context);
            orderRepository = new OrderRepository(context);

            productService = new ProductService(cart, productRepository, orderRepository, localizer);
        }

        [Fact]
        public void GetAllProductsTest()
        {   //Act
            var products = productService.GetAllProducts();

            //Assert
            Assert.NotEmpty(products);
        }
    }
    
}
