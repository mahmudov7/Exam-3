using Domain.Models;

namespace Domain.Interfaces;

public interface IProductRepository
{
    List<Product> GetAll();
    void GetById(int id);
    int AddProduct(Product product);
    bool UpdateProduct(Product product);
    bool DelateProduct(int id);
    void SearchByName(string term);
    void Filter(int? categoryId, decimal? minPrice, decimal? maxPrice);
}
