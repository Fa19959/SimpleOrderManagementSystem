using Microsoft.AspNetCore.Mvc;
using SimpleOrderManagementSystem.DTOs;
using SimpleOrderManagementSystem.Models;

namespace SimpleOrderManagementSystem.Services
{
    public interface IProductService
    {
        int AddProduct(ProductInputDTO input);

        List<ProductOutputDTO> GetProducts(decimal? minPrice, decimal? maxPrice, int pageNumber, int pageSize);

        Product GetProductByName(string name);

        void UpdateStocks(List<Product> products);


    }
}