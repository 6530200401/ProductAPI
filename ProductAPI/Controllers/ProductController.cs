using ProductAPI.Models;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Interfaces;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly string _json = "DB/MOCK_DATA.json";
        private readonly IManageProduct _ProductService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IManageProduct ProductService, ILogger<ProductController> logger) { 
            _ProductService = ProductService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult getAllProduct()
        {
            try
            {
                _logger.LogInformation("Get All Product");
                var response = _ProductService.GetAllProduct();
                _logger.LogInformation($"Success to Get {response.Count()} product");
                return Ok();
            }
            catch (Exception e) { 
                _logger.LogWarning(e, $"Error in {nameof(getAllProduct)}");
                return BadRequest("เกิดข้อผิดพลาด");
            }
        }

        [HttpGet("{id}")]
        public IActionResult getProductById(int id)
        {
            try
            {
                _logger.LogInformation($"Get Product By ID");
                var response = _ProductService.GetProductById(id);
                if (response == null)
                {
                    _logger.LogInformation($"Not Found ProductId {id}");
                    return NoContent();
                }
                _logger.LogInformation($"Success to Get ProductId {id}");
                return Ok(response);
            }
            catch(Exception e)
            {
                _logger.LogWarning(e, $"Error in {nameof(getProductById)}");
                return BadRequest("เกิดข้อผิดพลาด");
            }
           
        }

        [HttpPost]
        public IActionResult addProduct(NewProduct obj)
        {
            try
            {
                _logger.LogInformation("Add Product");
                if (ModelState.IsValid)
                {
                    _ProductService.addProduct(obj);
                    _logger.LogInformation("Success to Add Product");
                    return Ok();
                }
                _logger.LogWarning("Error in Input Field");
                return BadRequest(ModelState);
            }
            catch(Exception e)
            {
                _logger.LogWarning(e, $"Error in {nameof(addProduct)}");
                return BadRequest("เกิดข้อผิดพลาด");
            }
        }

        [HttpPut("{id}")]
        public IActionResult editProduct(int id, NewProduct obj)
        {
            try
            {
                _logger.LogInformation("Edit Product");
                if (ModelState.IsValid)
                {
                    bool response = _ProductService.editProduct(id, obj);

                    if (response)
                    {
                        _logger.LogInformation($"Success to Edit ProductId {id}");
                        return Ok();
                    }
                    _logger.LogInformation($"Not Found ProductId {id}");
                    return NoContent() ;
                }
                _logger.LogWarning("Error in Input Field");
                return BadRequest();
            }
            catch(Exception e) 
            {
                _logger.LogWarning(e, $"Error in {nameof(editProduct)}");
                return BadRequest("เกิดข้อผิดพลาด"); 
            }
        }

        [HttpDelete("{id}")]
        public IActionResult deleteProduct(int id)
        {
            try
            {
                _logger.LogInformation("Delete Product");
                bool response = _ProductService.deleteProduct(id);
                if (response)
                {
                    _logger.LogInformation($"Success to Delete ProductId {id}");
                    return Ok();
                }
                _logger.LogInformation($"Not Found ProductId {id}");
                return NoContent();
            }

            catch (Exception e)
            {
                _logger.LogWarning(e, $"Error in {nameof(deleteProduct)}");
                return BadRequest("เกิดข้อผิดพลาด");
            }
        }
    }
}
