using bilog.Models;

namespace bilog.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<string>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExist(string username, string email);
    }
}