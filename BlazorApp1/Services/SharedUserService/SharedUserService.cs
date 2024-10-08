using BlazorApp1.Model;
using BlazorApp1.Services.APIService;
using BlazorApp1.Services.UserService;

using MudBlazor;



namespace BlazorApp1.Services.SharedUserService
{
    public class SharedUserService 
    {
        public List<users> UserList { get; private set; }
        private readonly IUserService _iuserservice;

        public SharedUserService(IUserService iuserservice)
        {

            _iuserservice = iuserservice;
        }



        // This method loads the data if it's not already loaded
        public async Task LoadUsersAsync(IUserService _user)
        {
            if (UserList == null || !UserList.Any())
            {
                UserList = await _user.GetAlluser("Active");
            }
        }

        // Optionally provide a method to reset or reload the data
        public void ClearUserList()
        {
            UserList = null;
        }


    }
}
