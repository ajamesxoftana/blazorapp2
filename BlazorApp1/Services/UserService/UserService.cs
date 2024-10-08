using BlazorApp1.Services.APIService;
using BlazorApp1.Model;
using BlazorApp1.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorApp1.Services.UserService
{
    public class UserService : IUserService
    {

        private readonly IAPIService _apiService;
        private readonly HttpClient _httpClient;


        public UserService(IAPIService apiService, HttpClient httpClient)
        {
            _apiService = apiService;
            _httpClient = httpClient;
        }


        //public async Task<List<Users>> GetAllUsers(string stat)
        //{
        //    var users =  await _apiService.InvokeGet<List<Users>>($"api/User/GetAllUsers/" + stat);
        //    return users;
        //}

        public async Task<List<users>> GetAlluser(string stat)
        {
            var user = await _apiService.InvokeGet<List<users>>($"api/Users/GetAllUsers/" + stat);

            return user;
        }

       

        public async Task<users> AddnewUser(users newuser)
        {

            try
            {
                // Invoke the API service to add a new user and get the result
                var ret = await _apiService.InvokePost($"api/Users/AddnewUser", newuser);

                // Return the newly created user object
                return ret;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"Error adding new user: {ex.Message}");

                // Optionally, you can return null or rethrow the exception depending on the context
                throw new ApplicationException("Failed to add new user.", ex);
            }
        }


        public async Task<users> Edituser(users nusers)
        {
            try
            {
                // Invoke the API service to add a new user and get the result
                await _apiService.InvokePut($"api/Users/Edituser", nusers);

                // Return the newly created user object
                
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"Error adding new user: {ex.Message}");

                // Optionally, you can return null or rethrow the exception depending on the context
                throw new ApplicationException("Failed to add new user.", ex);
            }

           
            return null;

        }

        public async Task<bool> DeleteUser(int userId)
        {
            try
            {
                // Call the DELETE endpoint and check if the operation succeeded
                bool success = await _apiService.InvokeDelete($"api/Users/Deleteuser/{userId}");

                // Return the result of the deletion operation
                return success;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"Error deleting user with ID {userId}: {ex.Message}");

                // Return false in case of failure
                return false;
            }
        }


        //Method to fetch users
        public async Task<UsersResponse> GetUsersAsync(int page = 1, int perPage = 6)
        {
            var response = await _httpClient.GetFromJsonAsync<UsersResponse>($"https://reqres.in/api/users?page={page}&per_page={perPage}");
            return response;
        }


    }

}
