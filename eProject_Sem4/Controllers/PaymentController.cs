﻿using Business.Account;
using Business.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace eProject_Sem4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IPaymentHandler _paymentHandler;

        public PaymentController(IPaymentHandler paymentHandler)
        {
            _paymentHandler = paymentHandler;
        }

        /// <summary>
        /// lấy ra danh sách loại hình thanh toán 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getall-payment")]
        [ProducesResponseType(typeof(ResponseObject<List<PaymentModel>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPayment(PageModel model)
        {
            return Ok(await _paymentHandler.GetAllPayment(model));
        }

        /// <summary>
        /// thêm mới loại hình thanh toán 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insert-payment")]
        [ProducesResponseType(typeof(ResponseObject<PaymentModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertPayment(PaymentModel model)
        {
            return Ok(await _paymentHandler.InsertPayment(model));
        }

        /// <summary>
        /// cập nhật loại hình thanh toán 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update-payment")]
        [ProducesResponseType(typeof(ResponseObject<PaymentModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePayment(PaymentModel model)
        {
            return Ok(await _paymentHandler.UpdatePayment(model));
        }

        /// <summary>
        /// xóa loại hình thanh toán 
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("delete-payment-by-id")]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePaymentById(Guid? paymentId)
        {
            return Ok(await _paymentHandler.DeletePayment(paymentId));
        }

        /// <summary>
        /// xem thông tin chi tiết loại hình thanh toán 
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-payment-by-id")]
        [ProducesResponseType(typeof(ResponseObject<Guid>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaymentById(Guid? paymentId)
        {
            return Ok(await _paymentHandler.GetPaymentById(paymentId));
        }
    }
}
