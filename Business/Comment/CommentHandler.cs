using Business.Payment;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Comment
{
    public class CommentHandler : ICommentHandler
    {
        private readonly MyDB_Context _myDbContext;
        private readonly IConfiguration _config;
        private readonly ILogger<CommentHandler> _logger;

        public CommentHandler(MyDB_Context myDbContext, IConfiguration config, ILogger<CommentHandler> logger)
        {
            _myDbContext = myDbContext;
            _config = config;
            _logger = logger;
        }

        public async Task<Response> DeleteComment(Guid? commentId)
        {
            try
            {
                if(commentId ==  null)
                {
                    return new ResponseError(Code.BadRequest, $"Thông tin trường commentId Không được để trống!");
                }

                var data = await _myDbContext.Comment.FirstOrDefaultAsync(x => x.CommentId == commentId);
                if (data == null)
                {
                    return new ResponseError(Code.BadRequest, "Dữ liệu trống!");
                }

                _myDbContext.Comment.Remove(data);
                int rs = await _myDbContext.SaveChangesAsync();
                if(rs > 0)
                {
                    return new ResponseObject<Guid?>(commentId, $"{Message.DeleteSuccess}", Code.Success);
                }
                return new ResponseError(Code.ServerError, $"{Message.DeleteError}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Message.ErrorLogMessage);
                return new ResponseError(Code.ServerError, $"{ex.Message}");
            }
        }

        public async Task<Response> GetAllComment(PageModel model)
        {
            try
            {
                var data = await _myDbContext.Comment.ToListAsync();
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

                var dataMap = AutoMapperUtils.AutoMap<Data.DataModel.Comment, CommentModel>(data);
                return new ResponseObject<List<CommentModel>>(dataMap, $"{Message.GetDataSuccess}", Code.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Message.ErrorLogMessage);
                return new ResponseError(Code.ServerError, $"{ex.Message}");
            }
        }

        public  async Task<Response> InsertComment(CommentModel model)
        {
            try
            {
                model.CreatedDate = DateTime.Now;
                if(model.AccountId == null)
                {
                    return new ResponseError(Code.BadRequest, $"Thông tin trường accountId không được để trống!");
                }
                if(model.ProductId == null)
                {
                    return new ResponseError(Code.BadRequest, $"Thông tin trường productId không được để trống!");
                }
                var dataMap = AutoMapperUtils.AutoMap<CommentModel, Data.DataModel.Comment>(model);
                _myDbContext.Comment.Add(dataMap);
                int rs = await _myDbContext.SaveChangesAsync();
                if(rs > 1)
                {
                    return new ResponseObject<CommentModel>(model, $"{Message.CreateSuccess}",Code.Success);
                }
                return new ResponseError(Code.ServerError, $"{Message.CreateError}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + Message.ErrorLogMessage);
                return new ResponseError(Code.ServerError, $"{ex.Message}");
            }
        }

        public Task<Response> UpdateComment(CommentModel model)
        {
            throw new NotImplementedException();
        }
    }
}
