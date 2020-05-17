using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;

namespace P3AddNewFunctionalityDotNetCore.Models.Services
{
    public class ProductService : IProductService
    {
        private readonly ICart _cart;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IStringLocalizer<ProductService> _localizer;

        public ProductService(ICart cart, IProductRepository productRepository,
            IOrderRepository orderRepository, IStringLocalizer<ProductService> localizer)
        {
            _cart = cart;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _localizer = localizer;
        }
        public List<ProductViewModel> GetAllProductsViewModel() //to test as a unit test
        {
             
            IEnumerable<Product> productEntities = GetAllProducts();
            return MapToViewModel(productEntities);
        }

        private static List<ProductViewModel> MapToViewModel(IEnumerable<Product> productEntities) //unit test if i wanted to
        {
            List <ProductViewModel> products = new List<ProductViewModel>();
            foreach (Product product in productEntities)
            {
                products.Add(new ProductViewModel
                {
                    Id = product.Id,
                    Stock = product.Quantity.ToString(),
                    Price = product.Price.ToString(CultureInfo.InvariantCulture),
                    Name = product.Name,
                    Description = product.Description,
                    Details = product.Details
                });
            }

            return products;
        }

        public List<Product> GetAllProducts() //tested as integration test
        {
            IEnumerable<Product> productEntities = _productRepository.GetAllProducts();
            return productEntities?.ToList(); //This returns a list of products, saw it on debugger
        }

        public ProductViewModel GetProductByIdViewModel(int id) //tested as integration test
        {
            List<ProductViewModel> products = GetAllProductsViewModel().ToList();
            return products.Find(p => p.Id == id);
        }


        public Product GetProductById(int id) //tested as integration test
        {
            List<Product> products = GetAllProducts().ToList();
            return products.Find(p => p.Id == id);
        }

        public async Task<Product> GetProduct(int id) //tested as integration test
        {
            var product = await _productRepository.GetProduct(id);
            return product;
        }

        public async Task<IList<Product>> GetProducts() //tested as integration test
        {
            var products = await _productRepository.GetProducts();
            return products;
        }
        public void UpdateProductQuantities() //VOIDS NOT TO BE TESTED IN INTEGRATION TESTS
        {
            Cart cart = (Cart) _cart;
            foreach (CartLine line in cart.Lines)
            {
                _productRepository.UpdateProductStocks(line.Product.Id, line.Quantity);
            }
        }

        // TODO this is an example method, remove it for 
        public List<string> CheckProductModelErrors(ProductViewModel product)
        {
            List<string> modelErrors = new List<string>();
            if (product.Name == null || string.IsNullOrWhiteSpace(product.Name))
            {
                modelErrors.Add(_localizer["MissingName"]); //Mocked
            }

            if (product.Price == null || string.IsNullOrWhiteSpace(product.Price))
            {
                modelErrors.Add(_localizer["MissingPrice"]); //Mocked
            }

            if (!Double.TryParse(product.Price, out double pc))
            {
                modelErrors.Add(_localizer["PriceNotANumber"]); //Mocked
            }
            else
            {
                if (pc <= 0)
                    modelErrors.Add(_localizer["PriceNotGreaterThanZero"]); //Mocked
            }

            if (product.Stock == null || string.IsNullOrWhiteSpace(product.Stock))
            {
                modelErrors.Add(_localizer["MissingQuantity"]); //Mocked
            }

            if (!int.TryParse(product.Stock, out int qt))
            {
                modelErrors.Add(_localizer["StockNotAnInteger"]); //Mocked
            }
            else
            {
                if (qt <= 0)
                    modelErrors.Add(_localizer["StockNotGreaterThanZero"]); //Mocked
            }

            return modelErrors;
        }

        public void SaveProduct(ProductViewModel product) // ANOTHER VOID NOT TO BE TESTED
        {
            var productToAdd = MapToProductEntity(product); // test MapToProductEntity and _productRepository.SaveProduct(productToAdd)
            _productRepository.SaveProduct(productToAdd);
        }

        private static Product MapToProductEntity(ProductViewModel product)  //unit test if i wanted to
        {
            Product productEntity = new Product
            {
                Name = product.Name,
                Price = double.Parse(product.Price),
                Quantity = Int32.Parse(product.Stock),
                Description = product.Description,
                Details = product.Details
            };
            return productEntity;
        }

        public void DeleteProduct(int id) //ANOTHER VOID NOT TO BE TESTED
        {
            // TODO what happens if a product has been added to a cart and has been later removed from the inventory ?
            // delete the product form the cart by using the specific method
            // => the choice is up to the student
            _cart.RemoveLine(GetProductById(id));

            _productRepository.DeleteProduct(id);
        }
    }
}
