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

        public Task<Response> GetAllComment(PageModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Response> InsertComment(CommentModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateComment(CommentModel model)
        {
            throw new NotImplementedException();
        }
    }
}
