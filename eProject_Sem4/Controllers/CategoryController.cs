using Business.Category;
using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace eProject_Sem4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1", IgnoreApi = false)]

    public class CategoryController : Controller
    {
        private ICategoryHandler _categoryHandler;
        private readonly IEasyCachingProviderFactory _cacheFactory;

        public CategoryController(ICategoryHandler categoryHandler, IEasyCachingProviderFactory cacheFactory)
        {
            _categoryHandler = categoryHandler;
            _cacheFactory = cacheFactory;
        }

        /// <summary>
        ///  lấy ra danh sách danh mục
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getall-category")]
        [ProducesResponseType(typeof(ResponseObject<List<CategoryCreateModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategory([FromQuery] PageModel model)
        {
            return Ok(await _categoryHandler.getAllCategory(model));
        }

        /// <summary>
        ///  lấy danh mục theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-category-by-id")]
        [ProducesResponseType(typeof(ResponseObject<CategoryCreateModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var cachekey = $"CATEID_{id}";
            var provider = _cacheFactory.GetCachingProvider("default");
            var cacheResult = await provider.GetAsync<Response>(cachekey);
            if (cacheResult != null && cacheResult.HasValue)
            {
                return Ok(cacheResult.Value);
            }
            var result = await _categoryHandler.getCategoryById(id);
            if(result.Code == Code.Success )
            {
                await provider.SetAsync(cachekey, result, TimeSpan.FromMinutes(10));
            }
            return Ok(result);
        }

        /// <summary>
        ///  tìm kiếm danh mục
        /// </summary>
        /// <param name="name"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("search-category")]
        [ProducesResponseType(typeof(ResponseObject<CategoryCreateModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchCategory([FromQuery] PageModel model, string name, bool status)
        {
            return Ok(await _categoryHandler.searchCategory(model, name, status));
        }

        /// <summary>
        ///  thêm mới danh mục
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-category")]
        [ProducesResponseType(typeof(ResponseObject<CategoryCreateModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCategory(CategoryCreateModel model)
        {
            return Ok(await _categoryHandler.CreateCategory(model));
        }

        /// <summary>
        /// cập nhật danh mục
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update-category")]
        [ProducesResponseType(typeof(ResponseObject<CategoryCreateModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryCreateModel model)
        {
            return Ok(await _categoryHandler.UpdateCategory(model));
        }

        /// <summary>
        /// xóa danh mục
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delete-category")]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCategory(Guid? Id)
        {
            return Ok(await _categoryHandler.DeleteCategory(Id));
        }

        [HttpGet]
        [Route("get-category-image")]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public IActionResult GetImage(string image)
        {
            Byte[] b = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\wwwroot\\images\\" + image);
            return File(b, "image/jpeg");
        }
    }
}