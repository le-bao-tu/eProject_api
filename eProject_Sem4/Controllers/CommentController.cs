using Business.Comment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace eProject_Sem4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1", IgnoreApi = false)]
    public class CommentController : ControllerBase
    {
        private ICommentHandler _commentHandler;

        public CommentController(ICommentHandler commentHandler)
        {
            _commentHandler = commentHandler;
        }

        /// <summary>
        /// lấy ra danh sách comment 
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getAll-comment")]
        [ProducesResponseType(typeof(ResponseObject<List<CommentModel>>),StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllComment([FromQuery]PageModel mode)
        {
            return Ok(await _commentHandler.GetAllComment(mode));
        }


        /// <summary>
        /// thêm mới comment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insert-comment")]
        [ProducesResponseType(typeof(ResponseObject<CommentModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertComment([FromBody]CommentModel model)
        {
            return Ok(await _commentHandler.InsertComment(model));
        }


        /// <summary>
        ///  cập nhật comment 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("uppdate-comment")]
        [ProducesResponseType(typeof(ResponseObject<CommentModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateComment([FromBody]CommentModel model)
        {
            return Ok(await _commentHandler.UpdateComment(model));
        }

        /// <summary>
        ///  xóa comment 
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delete-comment")]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteComment(Guid? commentId)
        {
            return Ok(await _commentHandler.DeleteComment(commentId));
        }
    }
}
