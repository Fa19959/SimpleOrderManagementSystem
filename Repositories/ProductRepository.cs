using SimpleOrderManagementSystem.Models;

namespace SimpleOrderManagementSystem.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int AddProduct(Product product)
        {

            _context.Products.Add(product);
            _context.SaveChanges();
            return product.PId;

        }

       public  List<Product> GetProducts(decimal? minPrice, decimal? maxPrice, int pageNumber, int pageSize)
        {
                          
                 return    _context.Products.Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                                            .Skip( pageSize * ( pageNumber - 1 ))
                                            .Take(pageSize)
                                            .OrderBy(p => p.Price)
                                            .ToList();
        }

        public Product GetProductByName(string name) 
        {
            return _context.Products.Where(p => p.Name == name).FirstOrDefault();
        }

        public void UpdateStocks(List<Product> products)
        {
                 _context.UpdateRange(products);
        }

    }
}
