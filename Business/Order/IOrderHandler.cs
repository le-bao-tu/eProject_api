using Business.Order;
using Business.OrderDetail;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Order
{
    public interface IOrderHandler
    {
        /// <summary>
        /// Lấy danh sách Order
        /// </summary>
        /// <returns></returns>
        Task<Response> getAllOrder(PageModel model);

        Task<Response> getOrderByAccountId(Guid? accountId);

        Task<Response> getFinishedOrderByAccountId(Guid? accountId);

        /// <summary>
        /// Lấy Order theo Id
        /// </summary>
        /// <returns></returns>
        Task<Response> getOrderById(Guid? OrderId);

        /// <summary>
        /// Lọc Order
        /// </summary>
        /// <returns></returns>
        Task<Response> searchOrder(PageModel model);


        /// <summary>
        /// thêm mới đơn hàng
        /// </summary>
        /// <param name="OrderModel"></param>
        /// <returns></returns>

        Task<Response> CreateOrder(OrderCreateModel OrderModel);
        /// <summary>
        /// cập nhật đơn hàng
        /// </summary>
        /// <param name="OrderModel"></param>
        /// <returns></returns>
        Task<Response> UpdateOrder(OrderCreateModel OrderModel);


        /// <summary>
        /// xóa đơn hàng
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        Task<Response> DeleteOrder(Guid? OrderId);


        Task<Response> UpdateOrderState(Guid orderId, int state, string cancelationReason);
    }
}
