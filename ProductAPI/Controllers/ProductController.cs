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
        //private readonly ILogger<ProductController> _logger;

        public ProductController(IManageProduct ProductService) { 
            _ProductService = ProductService;
        }

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            try
            {
                return Ok(_ProductService.GetAllProduct());
            }
            catch (Exception) { 
                return BadRequest("เกิดข้อผิดพลาด");
            }

            //_logger.Log
        }

        [HttpGet("{id}")]
        public IActionResult getProductById(int id)
        {
            try
            {
                var response = _ProductService.GetProductById(id);
                if (response == null)
                {
                    return NoContent();
                }
                return Ok(response);
            }
            catch(Exception)
            {
                return BadRequest("เกิดข้อผิดพลาด");
            }
           
        }

        [HttpPost]
        public IActionResult addProduct(NewProduct obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ProductService.addProduct(obj);

                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch(Exception)
            {
                return BadRequest("เกิดข้อผิดพลาด");
            }
        }

        [HttpPut("{id}")]
        public IActionResult editProduct(int id, NewProduct obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool response = _ProductService.editProduct(id, obj);

                    if (response)
                    {
                        return Ok();
                    }
                    return NoContent() ;
                }
                return BadRequest();
            }
            catch(Exception) 
            { 
                return BadRequest("เกิดข้อผิดพลาด"); 
            }
        }

        [HttpDelete("{id}")]
        public IActionResult deleteProduct(int id)
        {
            try
            {
                bool response = _ProductService.deleteProduct(id);
                if (response)
                {
                    return Ok();
                }
                return NoContent();
            }

            catch (Exception)
            {
                return BadRequest("เกิดข้อผิดพลาด");
            }
        }
    }
}
