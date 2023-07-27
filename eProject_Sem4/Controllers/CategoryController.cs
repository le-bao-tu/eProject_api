using Business.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace eProject_Sem4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private ICategoryHandler _categoryHandler;

        public CategoryController(ICategoryHandler categoryHandler)
        {
            _categoryHandler = categoryHandler;
        }

        /// <summary>
        ///  lấy ra danh sách danh mục
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        /*[Authorize]*/
        [Route("getall-category")]
        [ProducesResponseType(typeof(ResponseObject<List<CategoryCreateModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategory([FromQuery] PageModel model)
        {
            return Ok(await _categoryHandler.getAllCategory(model));
        }

        /// <summary>
        ///  thêm mới danh mục
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-category-by-id")]
        [ProducesResponseType(typeof(ResponseObject<CategoryCreateModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            return Ok(await _categoryHandler.getCategoryById(id));
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
        public async Task<IActionResult> UpdateCategory(CategoryCreateModel model)
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
