using Business.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace eProject_Sem4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private IProductHandler _productHandler;

        public ProductController(IProductHandler productHandler)
        {
            _productHandler = productHandler;
        }

        [HttpGet]
        /*[Authorize]*/
        [Route("getall-product")]
        [ProducesResponseType(typeof(ResponseObject<List<ProductCreateModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProduct([FromQuery] PageModel model)
        {
            return Ok(await _productHandler.getAllProduct(model));
        }

        [HttpGet]
        [Route("get-product-by-id")]
        [ProducesResponseType(typeof(ResponseObject<ProductCreateModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            return Ok(await _productHandler.getProductById(id));
        }

        [HttpGet]
        [Route("search-product")]
        [ProducesResponseType(typeof(ResponseObject<ProductCreateModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchProduct
            ([FromQuery] PageModel model, string? name, int? quantity, float? priceMin, float? priceMax, string? address, bool status, Guid? categoryId)
        {
            return Ok(await _productHandler.searchProduct(model, name, quantity, priceMin, priceMax, address, status, categoryId));
        }

        [HttpPost]
        [Route("create-product")]
        [ProducesResponseType(typeof(ResponseObject<ProductCreateModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateProduct(ProductCreateModel model)
        {
            return Ok(await _productHandler.CreateProduct(model));
        }

        [HttpPost]
        [Route("update-product")]
        [ProducesResponseType(typeof(ResponseObject<ProductCreateModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateProduct(ProductCreateModel model)
        {
            return Ok(await _productHandler.UpdateProduct(model));
        }

        [HttpGet]
        [Route("delete-product")]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProduct(Guid? Id)
        {
            return Ok(await _productHandler.DeleteProduct(Id));
        }

        [HttpGet]
        [Route("get-product-image")]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public IActionResult GetImage(string image)
        {
            Byte[] b = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\wwwroot\\images\\" + image);
            return File(b, "image/jpeg");
        }

    }
}
