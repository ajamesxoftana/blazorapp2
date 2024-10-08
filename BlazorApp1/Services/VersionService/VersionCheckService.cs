using System.Net.Http.Json;
using BlazorApp1.DTO;



namespace BlazorApp1.Services.VersionService
{
    public class VersionCheckService : IVersionCheckService
    {

        private readonly HttpClient _httpClient;
        private string _currentVersion = "1.0.0";

        public VersionCheckService(HttpClient httpClient)
        {

            _httpClient = httpClient;

        }

        public async Task CheckForUpdate()
        {
            var versionInfo = await _httpClient.GetFromJsonAsync<VersionInfo>("version.json");
            if (versionInfo != null && versionInfo.Version != _currentVersion)
            {
                // Notify the user that an update is available
                Console.WriteLine("New version available! Please refresh the page.");
            }
        }

    }
}
