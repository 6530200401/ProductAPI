using ProductAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
//using ProductAPI.Interfaces;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly string _json = "DB/MOCK_DATA.json";

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProduct()
        {
            var jsonProduct = System.IO.File.ReadAllText(_json);
            var products = JsonConvert.DeserializeObject<List<Product>>(jsonProduct);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult getProductById(int id)
        {
            var jsonProduct = System.IO.File.ReadAllText(_json);
            var products = JsonConvert.DeserializeObject<List<Product>>(jsonProduct);

            var existProduct = products.FirstOrDefault(p => p.ProductId == id);
            if (existProduct == null)
            {
                return NoContent();
            }
            return Ok(existProduct);
        }

        [HttpPost]
        public IActionResult addProduct(NewProduct obj)
        {
            if (ModelState.IsValid)
            {
                var jsonProduct = System.IO.File.ReadAllText(_json);
                var products = JsonConvert.DeserializeObject<List<Product>>(jsonProduct) ?? new List<Product>();
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

                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult editProduct(int id, NewProduct obj)
        {
            if (ModelState.IsValid)
            {
                var jsonProduct = System.IO.File.ReadAllText(_json);
                var products = JsonConvert.DeserializeObject<List<Product>>(jsonProduct) ?? new List<Product>();

                var existProduct = products.FirstOrDefault(p => p.ProductId == id);
                if (existProduct == null)
                {
                    return NotFound();
                }

                existProduct.Name = obj.Name;
                existProduct.Price = obj.Price;

                var updatedJson = JsonConvert.SerializeObject(products, Formatting.Indented);
                System.IO.File.WriteAllText(_json, updatedJson);
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult deleteProduct(int id)
        {

            var jsonProduct = System.IO.File.ReadAllText(_json);
            var products = JsonConvert.DeserializeObject<List<Product>>(jsonProduct) ?? new List<Product>();

            var existProduct = products.FirstOrDefault(p => p.ProductId == id);
            if (existProduct == null)
            {
                return NoContent();
            }

            products.Remove(existProduct);
            var updatedJson = JsonConvert.SerializeObject(products, Formatting.Indented);
            System.IO.File.WriteAllText(_json, updatedJson);

            return Ok();
        }
    }
}
