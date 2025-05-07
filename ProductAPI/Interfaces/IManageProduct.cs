using ProductAPI.Models;

namespace ProductAPI.Interfaces
{
    public interface IManageProduct
    {
        IEnumerable<Product> GetAllProduct();

        Product GetProductById(int id);

        void addProduct(NewProduct obj);

        bool editProduct(int id, NewProduct obj);

        bool deleteProduct(int id);
    }
}
