using Business.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace eProject_Sem4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1", IgnoreApi = false)]
    public class UserController : ControllerBase
    {

        private IUserHandler _userHandler;

        public UserController(IUserHandler userHandler)
        {
            _userHandler = userHandler;
        }

        /// <summary>
        /// api đăng nhập
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(ResponseObject<String>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(UserModel userModel)
        {
            return Ok(await _userHandler.Login(userModel));
        }

        /// <summary>
        /// Lấy ra danh sách tài khoản 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getAll-user")]
        [ProducesResponseType(typeof(ResponseObject<List<UserCreateModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(await _userHandler.GetAllUser());
        }

        /// <summary>
        /// create User
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-user")]
        [ProducesResponseType(typeof(ResponseObject<UserCreateModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateUser(UserCreateModel userModel)
        {
            return Ok(await _userHandler.CreateUser(userModel));
        }

        /// <summary>
        /// lấy pass code
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("send-passcode")]
        [ProducesResponseType(typeof(ResponseObject<String>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SendPassCode(string email)
        {
            return Ok(await _userHandler.GetAccessCode(email));
        }

        /// <summary>
        /// check pass code
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("check-passcode")]
        [ProducesResponseType(typeof(ResponseObject<String>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckPassCode(Guid? userId ,string passcode)
        {
            return Ok(await _userHandler.CheckPassCode(userId,passcode));
        }

        /// <summary>
        /// thay doi mat khau
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("change-password")]
        [ProducesResponseType(typeof(ResponseObject<String>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangePassword(Guid? userId, string password)
        {
            return Ok(await _userHandler.ChangePassword(userId, password));
        }

        /// <summary>
        /// lấy tên bởi token
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-by-name-token")]
        [ProducesResponseType(typeof(ResponseObject<UserCreateModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByNameToken(string email)
        {
            return Ok(await _userHandler.GetByNameToken(email));
        }

        /// <summary>
        /// cập nhật user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update-user")]
        [ProducesResponseType(typeof(ResponseObject<UserCreateModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser(UserCreateModel model)
        {
            return Ok(await _userHandler.UpdateUser(model));
        }

        /// <summary>
        /// xóa tài khoản
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("delete-user")]
        [ProducesResponseType(typeof(ResponseObject<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUser(Guid? userId)
        {
            return Ok(await _userHandler.DeleteUser(userId));
        }
    }
}
