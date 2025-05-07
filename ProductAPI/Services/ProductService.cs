using Newtonsoft.Json;
using ProductAPI.Interfaces;
using ProductAPI.Models;

namespace ProductAPI.Services
{
    public class ProductService : IManageProduct
    {
        private readonly string _json = "DB/MOCK_DATA.json";

        public IEnumerable<Product> GetAllProduct()
        {
            var jsonProduct = System.IO.File.ReadAllText(_json);
            var products = JsonConvert.DeserializeObject<List<Product>>(jsonProduct);
            return products;
        }

        public Product GetProductById(int id)
        {
            var jsonProduct = System.IO.File.ReadAllText(_json);
            var products = JsonConvert.DeserializeObject<List<Product>>(jsonProduct);
            var existProduct = products.FirstOrDefault(p => p.ProductId == id);

            return existProduct;
        }

        public void addProduct(NewProduct obj) 
        {
            var jsonProduct = System.IO.File.ReadAllText(_json);
            var products = JsonConvert.DeserializeObject<List<Product>>(jsonProduct);
            int lastProductId = products.Max(p => p.ProductId);

            var newProductObj = new Product
            {
                ProductId = lastProductId + 1,
                Name = obj.Name,
                Price = obj.Price
            };
            products.Add(newProductObj);

            var updatedJson = JsonConvert.SerializeObject(products, Formatting.Indented);
            System.IO.File.WriteAllText(_json, updatedJson);
        }

        public bool editProduct(int id, NewProduct obj) 
        {
            var jsonProduct = System.IO.File.ReadAllText(_json);
            var products = JsonConvert.DeserializeObject<List<Product>>(jsonProduct);
            var existProduct = products.FirstOrDefault(p => p.ProductId == id);

            if (existProduct == null)
            {
                return false;
            }

            existProduct.Name = obj.Name;
            existProduct.Price = obj.Price;

            var updatedJson = JsonConvert.SerializeObject(products, Formatting.Indented);
            System.IO.File.WriteAllText(_json, updatedJson);
            return true;
        }

        public bool deleteProduct(int id)
        {
            var jsonProduct = System.IO.File.ReadAllText(_json);
            var products = JsonConvert.DeserializeObject<List<Product>>(jsonProduct);

            var existProduct = products.FirstOrDefault(p => p.ProductId == id);

            if(existProduct == null)
            {
                return false;
            }

            products.Remove(existProduct);
            var updatedJson = JsonConvert.SerializeObject(products, Formatting.Indented);
            System.IO.File.WriteAllText(_json, updatedJson);
            return true;
        }
    }
}
