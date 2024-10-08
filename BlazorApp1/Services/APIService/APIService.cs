using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorApp1.Services.APIService
{
    public class APIService : IAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseURL;


        public APIService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            // Ensure BaseAddress is not null
            _baseURL = _httpClient.BaseAddress?.ToString() ?? string.Empty;

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<T> InvokeGetAsync<T>(string uri)
        {
            return await _httpClient.GetFromJsonAsync<T>(uri);
        }
        public async Task<T> InvokeRetPostAsync<T, U>(string uri, U obj)
        {
            var response = await _httpClient.PostAsJsonAsync(uri, obj);
            return await response.Content.ReadFromJsonAsync<T>();
        }
        public async Task<T> InvokeGet<T>(string uri)
        {
            return await _httpClient.GetFromJsonAsync<T>(uri);
        }

        public async Task<byte[]> InvokeGetPDF(string uri)
        {
            return await _httpClient.GetByteArrayAsync(uri);
        }


        public async Task<T> InvokePost<T>(string uri, T obj)
        {
            //var response = await _httpClient.PostAsJsonAsync(uri, obj);
            //response.EnsureSuccessStatusCode();
            //return await response.Content.ReadFromJsonAsync<T>();


            try
            {
                // Make the POST request and get the response
                var response = await _httpClient.PostAsJsonAsync(uri, obj);

                // Ensure the response indicates success
                response.EnsureSuccessStatusCode();

                // Check if the response has content
                if (response.Content.Headers.ContentLength == 0)
                {
                    // If no content is expected (like with a 204 No Content), return default(T)
                    return default(T);
                }

                // Attempt to deserialize the response content into T
                var result = await response.Content.ReadFromJsonAsync<T>();

                // Handle null result (if the API returns no content or deserialization fails)
                if (result == null)
                {
                    throw new InvalidOperationException("The response body was empty or could not be deserialized.");
                }

                return result;
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request errors
                throw new ApplicationException($"Error posting to {uri}: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Catch other possible exceptions (serialization issues, etc.)
                throw new ApplicationException($"An error occurred during the POST operation: {ex.Message}", ex);
            }

        }

        public async Task InvokePut<T>(string uri, T obj)
        {
            var response = await _httpClient.PutAsJsonAsync(uri, obj);
            response.EnsureSuccessStatusCode();
        }

        //public async Task InvokeDelete<T>(string uri)
        //{
        //    var response = await _httpClient.DeleteAsync(uri);
        //    response.EnsureSuccessStatusCode();
        //}

        public async Task<bool> InvokeDelete(string uri)
        {
            try
            {
                // Send DELETE request to the provided URI
                var response = await _httpClient.DeleteAsync(uri);

                // Ensure that the response is successful
                response.EnsureSuccessStatusCode();

                // Return true if the deletion is successful
                return true;
            }
            catch (HttpRequestException ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"Error during DELETE request: {ex.Message}");

                // Return false if the deletion failed
                return false;
            }
        }

        private string GetURL(string uri)
        {
            return $"{_baseURL}/{uri}";
        }
    }
}
