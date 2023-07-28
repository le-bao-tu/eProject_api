﻿using Business.AddressAccount;
using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace eProject_Sem4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1", IgnoreApi = false)]
    public class AddressController : ControllerBase
    {
        private IAddressAccountHandler _addressAccountHandler;
        private readonly IEasyCachingProviderFactory _cacheFactory;

        public AddressController(IAddressAccountHandler addressAccountHandler, IEasyCachingProviderFactory cacheFactory)
        {
            _addressAccountHandler = addressAccountHandler;
            _cacheFactory = cacheFactory;
        }

        /// <summary>
        /// lấy ra danh sách địa chỉ account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getall-address-account")]
        [ProducesResponseType(typeof(ResponseObject<List<AddressAccountModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAddressAccount(PageModel model)
        {
            return Ok(await _addressAccountHandler.GetAllAddressAccount(model));
        }

        /// <summary>
        /// thêm mới địa chỉ Account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insert-address-account")]
        [ProducesResponseType(typeof(ResponseObject<AddressAccountModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertAddressAccount(AddressAccountModel model)
        {
            return Ok(await _addressAccountHandler.InsertAddressAccount(model));
        }

        /// <summary>
        /// cập nhật địa chỉ account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update-address-account")]
        [ProducesResponseType(typeof(ResponseObject<AddressAccountModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAddressAccount(AddressAccountModel model)
        {
            return Ok(await _addressAccountHandler.UpdateAddressAccount(model));
        }

        /// <summary>
        /// xóa địa chỉ account
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delete-address-account")]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAddressAccount(Guid? addressId)
        {
            return Ok(await _addressAccountHandler.DeleteAddressAccount(addressId));
        }

        /// <summary>
        /// lấy chi tiết địa chỉ
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-address-by-id")]
        [ProducesResponseType(typeof(ResponseObject<AddressAccountModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAddressById(Guid? addressId)
        {
            var cachekey = $"ADDRESSID_{addressId}";
            var provider = _cacheFactory.GetCachingProvider("default");
            var cacheResult = await provider.GetAsync<Response>(cachekey);
            if(cacheResult != null && cacheResult.HasValue)
            {
                return Ok(cacheResult.Value);
            }
            var result = await _addressAccountHandler.GetAddressAccountById(addressId);
            if(result.Code == Code.Success)
            {
                await provider.SetAsync(cachekey, result, TimeSpan.FromHours(10));
            }
            return Ok(result);
        }

        /// <summary>
        /// lấy danh sách địa chỉ theo accountId
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-address-by-accountId")]
        [ProducesResponseType(typeof(ResponseObject<List<AddressAccountModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAddressByAccountId(Guid? accountId)
        {
            return Ok(await _addressAccountHandler.GetAddressByAccountId(accountId));
        }
    }
}