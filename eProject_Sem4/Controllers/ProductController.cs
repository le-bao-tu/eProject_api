using Business.Order;
using Business.Product;
using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace eProject_Sem4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1", IgnoreApi = false)]
    public class ProductController : Controller
    {
        private IProductHandler _productHandler;
        private readonly IEasyCachingProviderFactory _cacheFactory;

        public ProductController(IProductHandler productHandler, IEasyCachingProviderFactory factory)
        {
            _productHandler = productHandler;
            _cacheFactory = factory;
        }

        [HttpGet]
        /*[Authorize]*/
        [Route("getall-product")]
        [ProducesResponseType(typeof(ResponseObject<List<ProductCreateModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProduct([FromQuery] PageModel model)
        {
            return Ok(await _productHandler.getAllProduct(model));
        }

        /// <summary>
        /// sắp xếp 
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("sortby-product")]
        [ProducesResponseType(typeof(ResponseObject<List<ProductCreateModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SortByProduct([FromQuery] string sort)
        {
            return Ok(await _productHandler.SortBy(sort));
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

        /// <summary>
        /// lấy sản phẩm theo danh mục
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-product-by-cate")]
        [ProducesResponseType(typeof(ResponseObject<List<ProductCreateModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductByCate(Guid? cateId)
        {
            var cachekey = $"CATEID_{cateId}";
            var provider = _cacheFactory.GetCachingProvider("default");
            var cacheResult = await provider.GetAsync<Response>(cachekey);
            if (cacheResult != null && cacheResult.HasValue)
            {
                return Ok(cacheResult.Value);
            }
            var result = await _productHandler.GetProductByCate(cateId);
            if (result.Code == Code.Success)
            {
                await provider.SetAsync(cachekey, result, TimeSpan.FromMinutes(10));
            }
            return Ok(result);
        }
    }
}