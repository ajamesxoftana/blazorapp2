using BlazorApp1.DTO;
using BlazorApp1.Model;

using System.Collections.Generic;
using System.Threading.Tasks;


namespace BlazorApp1.Services.UserService
{
    public interface IUserService
    {
        Task<users> AddnewUser(users newuser);
        Task<List<users>> GetAlluser(string status);

        Task<users> Edituser(users nusers);
       // Task<bool> InvokeDelete(string uri);

        Task<bool> DeleteUser(int userId);

        Task<UsersResponse> GetUsersAsync(int page = 1, int perPage = 6);
    }
}