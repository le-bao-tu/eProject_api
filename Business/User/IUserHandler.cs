using Shared;

namespace Business.User
{
    public interface IUserHandler
    {
        /// <summary>
        /// đăng nhập 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        Task<Response> Login(UserModel userModel);

        /// <summary>
        /// thêm mới tài khoản 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        Task<Response> CreateUser(UserCreateModel userModel);
        /// <summary>
        /// cập nhật tài khoản 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        Task<Response> UpdateUser(UserCreateModel userModel);
        /// <summary>
        /// lấy mã code 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<Response> GetAccessCode(string email);

        /// <summary>
        /// check passcode
        /// </summary>
        /// <param name="passcode"></param>
        /// <returns></returns>
        Task<Response> CheckPassCode(string passcode);

        /// <summary>
        /// change password 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<Response> ChangePassword(Guid? userId, string password);

        /// <summary>
        /// get by name Token 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<Response> GetByNameToken(string email);

        /// <summary>
        /// xóa tài khoản 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Response> DeleteUser(string userId);
    }
}