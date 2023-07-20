﻿using Business.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace eProject_Sem4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountHandler _accountHandler;

        public AccountController(IAccountHandler accountHandler)
        {
            _accountHandler = accountHandler;
        }

        /// <summary>
        ///  lấy ra danh scahs tài khoản  
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("getall-account")]
        [ProducesResponseType(typeof(ResponseObject<List<AccountCreateModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAccount(PageModel model)
        {
            return Ok(await _accountHandler.GetAllAccount(model));
        }

        /// <summary>
        ///  đăng ký tài khoản 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("signup-account")]
        [ProducesResponseType(typeof(ResponseObject<AccountCreateModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SignUpAccount(AccountCreateModel model)
        {
            return Ok(await _accountHandler.SingUpAccount(model));
        }

        /// <summary>
        /// đăng nhập 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(ResponseObject<AccountModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(AccountModel model)
        {
            return Ok(await _accountHandler.Login(model));
        }

        /// <summary>
        /// cập nhật tài khoản 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update-account")]
        [ProducesResponseType(typeof(ResponseObject<AccountCreateModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAccount(AccountCreateModel model)
        {
            return Ok(await _accountHandler.UpdateAccount(model));
        }

        /// <summary>
        /// xóa tài khoản người dùng 
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delete-account")]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAccount(Guid? accountId)
        {
            return Ok(await _accountHandler.DeleteAccount(accountId));
        }

        /// <summary>
        /// lấy tài khoản theo Id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-account-by-id")]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccountById(Guid? accountId)
        {
            return Ok(await _accountHandler.DeleteAccount(accountId));
        }

        /// <summary>
        /// lấy thông tin người dùng theo email 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-account-by-email")]
        [ProducesResponseType(typeof(ResponseObject<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccountByEmail(string email)
        {
            return Ok(await _accountHandler.GetAccountByEmail(email));
        }

        /// <summary>
        /// lấy pascode 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-access-code")]
        [ProducesResponseType(typeof(ResponseObject<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAccessCode(string email)
        {
            return Ok(await _accountHandler.GetAccessCode(email));
        }

        /// <summary>
        /// check pascode 
        /// </summary>
        /// <param name="passcode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("check-pass-code")]
        [ProducesResponseType(typeof(ResponseObject<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckPassCode(string passcode)
        {
            return Ok(await _accountHandler.CheckPassCode(passcode));
        }

        /// <summary>
        /// thay đổi mật khẩu 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("change-password")]
        [ProducesResponseType(typeof(ResponseObject<bool>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangePassword(Guid? userId, string password)
        {
            return Ok(await _accountHandler.ChangePassword(userId,password));
        }
    }
}
