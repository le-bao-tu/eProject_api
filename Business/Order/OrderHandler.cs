﻿using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shared;

namespace Business.Order
{
    public class OrderHandler : IOrderHandler
    {
        private readonly MyDB_Context _myDbContext;
        private readonly IConfiguration _config;
        private readonly ILogger<OrderHandler> _logger;

        public OrderHandler(MyDB_Context myDbContext, IConfiguration config, ILogger<OrderHandler> logger)
        {
            _myDbContext = myDbContext;
            _config = config;
            _logger = logger;
        }

        public async Task<Response> getAllOrder(PageModel model)
        {
            try
            {
                var data = await _myDbContext.Order.Include(x => x.Account).ToListAsync();
                if (model.PageSize.HasValue && model.PageNumber.HasValue)
                {
                    if (model.PageSize <= 0)
                    {
                        model.PageSize = 0;
                    }

                    int excludeRows = (model.PageNumber.Value - 1) * (model.PageSize.Value);
                    if (excludeRows <= 0)
                    {
                        excludeRows = 0;
                    }
                    data = data.Skip(excludeRows).Take(model.PageSize.Value).ToList();
                }
                var dataMap = AutoMapperUtils.AutoMap<Data.DataModel.Order, OrderCreateModel>(data);
                return new ResponseObject<List<OrderCreateModel>>(dataMap, $"{Message.GetDataSuccess}", Code.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Message.ErrorLogMessage);
                return new ResponseError(Code.ServerError, $"{ex.Message}");
            }
        }

        public async Task<Response> getOrderById(Guid? OrderId)
        {
            try
            {
                if (OrderId == null)
                {
                    return new ResponseError(Code.BadRequest, "Thông tin trường OrderId không được để trống!");
                }

                var data = await _myDbContext.Order.Include(x => x.Account).FirstOrDefaultAsync(x => x.OrderId.Equals(OrderId));
                if (data == null)
                {
                    return new ResponseError(Code.ServerError, "Không tồn tại thông tin đơn hàng!");
                }

                var dataMap = AutoMapperUtils.AutoMap<Data.DataModel.Order, OrderCreateModel>(data);
                return new ResponseObject<OrderCreateModel>(dataMap, $"{Message.GetDataSuccess}", Code.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Message.ErrorLogMessage);
                return new ResponseError(Code.ServerError, $"{ex.Message}");
            }
        }

        public Task<Response> searchOrder(PageModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> CreateOrder(OrderCreateModel OrderModel)
        {
            try
            {
                var validation = new ValidationOrderModel();
                var result = await validation.ValidateAsync(OrderModel);
                if (!result.IsValid)
                {
                    var errorMessage = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return new ResponseError(Code.ServerError, "Dữ liệu không hợp lệ!", errorMessage);
                }

                OrderModel.CreatedDate = DateTime.Now;

                var dataMap = AutoMapperUtils.AutoMap<OrderCreateModel, Data.DataModel.Order>(OrderModel);
                _myDbContext.Order.Add(dataMap);
                int rs = await _myDbContext.SaveChangesAsync();
                if (rs > 0)
                {
                    foreach (var OD in OrderModel.ListOrderDetail)
                    {
                        var odcm = new Data.DataModel.OrderDetail();
                        odcm.OrderId = OrderModel.OrderId;
                        odcm.ProductId = OD.ProductId;
                        odcm.price = OD.price;
                        odcm.Quantity = OD.Quantity;

                        _myDbContext.OrderDetail.Add(odcm);
                        int rs1 = await _myDbContext.SaveChangesAsync();
                        if (rs1 > 0)
                        {
                            var pro = await _myDbContext.Product.FirstOrDefaultAsync(x => x.ProductId == odcm.ProductId);
                            if (pro != null)
                            {
                                pro.Quantity = pro.Quantity - odcm.Quantity;
                                _myDbContext.Product.Update(pro);
                                await _myDbContext.SaveChangesAsync();
                            }

                            _logger.LogError("Thêm mới chi tiết đơn hàng thành công", Code.ServerError);
                        }
                    }

                    _logger.LogInformation("Thêm mới đơn hàng thành công", OrderModel);
                    return new ResponseObject<OrderCreateModel>(OrderModel, "Thêm mới đơn hàng thành công", Code.Success);
                }
                else
                {
                    _logger.LogError("Thêm mới đơn hàng thất bại", OrderModel);
                    return new ResponseError(Code.ServerError, "Thêm mới đơn hàng thất bại");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Message.ErrorLogMessage);
                return new ResponseError(Code.ServerError, $"{Message.CreateError} - {ex.Message}");
            }
        }

        public async Task<Response> UpdateOrder(OrderCreateModel OrderModel)
        {
            try
            {
                var validation = new ValidationOrderModel();
                var result = await validation.ValidateAsync(OrderModel);
                if (!result.IsValid)
                {
                    var errorMessage = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return new ResponseError(Code.ServerError, "Dữ liệu không hợp lệ!", errorMessage);
                }

                var data = await _myDbContext.Order.FirstOrDefaultAsync(x => x.OrderId.Equals(OrderModel.OrderId));
                if (data == null)
                {
                    return new ResponseError(Code.BadRequest, "đơn hàng không tồn tại");
                }
                else
                {
                    data.OrderId = OrderModel.OrderId;
                    data.TotalPrice = OrderModel.TotalPrice;
                    data.Phone = OrderModel.Phone;
                    data.Email = OrderModel.Email;
                    data.Address = OrderModel.Address;
                    data.AccountId = OrderModel.AccountId;
                    data.State = OrderModel.State;
                    data.CancellationReason = OrderModel.CancellationReason;
                    data.Feedback = OrderModel.Feedback;
                    data.UpdatedDate = DateTime.Now;

                    _myDbContext.Order.Update(data);
                    int rs = await _myDbContext.SaveChangesAsync();
                    if (rs > 0)
                    {
                        return new ResponseObject<OrderCreateModel>(OrderModel, $"{Message.UpdateSuccess}", Code.Success);
                    }

                    return new ResponseError(Code.ServerError, $"{Message.UpdateError}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Message.ErrorLogMessage);
                return new ResponseError(Code.ServerError, $"{ex.Message}");
            }
        }

        public async Task<Response> DeleteOrder(Guid? OrderId)
        {
            try
            {
                if (OrderId == null)
                {
                    return new ResponseError(Code.BadRequest, "Thông tin trường OrderId không được để trống!");
                }

                var data = await _myDbContext.Order.FirstOrDefaultAsync(x => x.OrderId.Equals(OrderId));
                if (data == null)
                {
                    return new ResponseError(Code.BadRequest, "đơn hàng không tồn tại trong hệ thống!");
                }

                var listOd = await _myDbContext.OrderDetail.Where(x => x.OrderId.Equals(OrderId)).ToListAsync();

                if (listOd != null)
                {
                    _myDbContext.OrderDetail.RemoveRange(listOd);
                    int rs1 = await _myDbContext.SaveChangesAsync();

                    if (rs1 > 0)
                    {
                        _myDbContext.Order.Remove(data);
                        int rs = await _myDbContext.SaveChangesAsync();
                        if (rs > 0)
                        {
                            return new ResponseObject<Guid?>(OrderId, $"Xóa đơn hàng thành công : {OrderId}", Code.Success);
                        }
                    }

                    return new ResponseError(Code.ServerError, "Xóa đơn hàng thất bại");
                }

                return new ResponseError(Code.ServerError, "Xóa đơn hàng thất bại");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Message.ErrorLogMessage);
                return new ResponseError(Code.ServerError, $"{ex.Message}");
            }
        }

        public async Task<Response> UpdateOrderState(Guid orderId, int state, string cancelationReason)
        {
            try
            {
                var data = await _myDbContext.Order.FirstOrDefaultAsync(x => x.OrderId.Equals(orderId));
                if (data == null)
                {
                    return new ResponseError(Code.BadRequest, "đơn hàng không tồn tại");
                }
                else
                {
                    data.OrderId = orderId;
                    data.State = state;
                    data.CancellationReason = cancelationReason;

                    _myDbContext.Order.Update(data);
                    int rs = await _myDbContext.SaveChangesAsync();
                    if (rs > 0)
                    {
                        return new ResponseObject<Data.DataModel.Order>(data, $"{Message.UpdateSuccess}", Code.Success);
                    }

                    return new ResponseError(Code.ServerError, $"{Message.UpdateError}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Message.ErrorLogMessage);
                return new ResponseError(Code.ServerError, $"{ex.Message}");
            }
        }

        public async Task<Response> getOrderByAccountId(Guid? accountId)
        {
            try
            {
                if (accountId == null)
                {
                    return new ResponseError(Code.BadRequest, "Thông tin trường accountId không được để trống!");
                }

                var data = await _myDbContext.Order.Where(x => x.AccountId.Equals(accountId)).ToListAsync();
                if (data == null)
                {
                    return new ResponseError(Code.ServerError, "Không tồn tại thông tin đơn hàng!");
                }

                var dataMap = AutoMapperUtils.AutoMap<Data.DataModel.Order, OrderCreateModel>(data);
                return new ResponseObject<List<OrderCreateModel>>(dataMap, $"{Message.GetDataSuccess}", Code.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Message.ErrorLogMessage);
                return new ResponseError(Code.ServerError, $"{ex.Message}");
            }
        }

        public async Task<Response> getFinishedOrderByAccountId(Guid? accountId)
        {
            try
            {
                if (accountId == null)
                {
                    return new ResponseError(Code.BadRequest, "Thông tin trường accountId không được để trống!");
                }

                // item.state  == 1 ? 'Đặt hàng thành công' :
                // item.state  == 2 ? 'Đang giao hàng' :
                // item.state  == 3 ? 'Giao hàng thành công' : 'Đã hủy'
                var data = await _myDbContext.Order.Where(x => x.AccountId.Equals(accountId) && x.State == 3).ToListAsync();
                if (data == null)
                {
                    return new ResponseError(Code.ServerError, "Không tồn tại thông tin đơn hàng!");
                }

                var dataMap = AutoMapperUtils.AutoMap<Data.DataModel.Order, OrderCreateModel>(data);
                return new ResponseObject<List<OrderCreateModel>>(dataMap, $"{Message.GetDataSuccess}", Code.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Message.ErrorLogMessage);
                return new ResponseError(Code.ServerError, $"{ex.Message}");
            }
        }
    }
}