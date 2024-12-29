using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SimpleOrderManagementSystem.DTOs;
using SimpleOrderManagementSystem.Models;
using SimpleOrderManagementSystem.Repositories;

namespace SimpleOrderManagementSystem.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _ProductRepository;

        public ProductService(IProductRepository repository)
        {
            _ProductRepository = repository;
        }

        public int AddProduct(ProductInputDTO input)
        {

            var product = new Product
            {

                Name = input.Name,
                Description = input.Description,
                Stock = input.Stock,
                Price = input.Price,
                CreatedAt = DateTime.Now,
            };

            return _ProductRepository.AddProduct(product);

        }

        public List<ProductOutputDTO> GetProducts(decimal? minPrice, decimal? maxPrice, int pageNumber, int pageSize)
        {
            minPrice = minPrice ?? 0;
            maxPrice = maxPrice ?? 100;

            var products =_ProductRepository.GetProducts(minPrice, maxPrice, pageNumber, pageSize);
            var outputList = new List<ProductOutputDTO>();
            foreach (var product in products)
            {
                outputList.Add(
                                     new ProductOutputDTO { ProductName=product.Name, price=product.Price, stock=product.Stock}
                              );
          
            };
            
            return outputList;
        }

       public Product GetProductByName(string name)
        {
            return _ProductRepository.GetProductByName(name);   
        }

        public void UpdateStocks(List<Product> products)
        {
            _ProductRepository.UpdateStocks(products);
        }

    }
}
