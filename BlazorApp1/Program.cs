using BlazorApp1;
using BlazorApp1.Services.APIService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Microsoft.AspNetCore.SignalR.Client;


using System.Threading.Tasks;
using System;
using BlazorApp1.Services.UserService;
using Blazored.LocalStorage;
using System.Net.Http;
using Microsoft.JSInterop;

using BlazorApp1.Services.LocalStorageServices;
using BlazorApp1.Services.SharedUserService;
using BlazorApp1.Services.VersionService;
using BlazorApp1.Services.AppUpdateService;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

///Services
builder.Services.AddScoped<IAPIService, APIService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<SharedUserService>();
builder.Services.AddScoped<IVersionCheckService, VersionCheckService>();
builder.Services.AddScoped<AppUpdateService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddTransient<LocalStorageServices>();


// Register HttpClient for dependency injection (DI)
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


///////  Local API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7039") });
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

//builder.Services.AddScoped<AppUpdateService>();

// In the App component or another service, handle update events
//public class AppUpdateService
//{
//    public event Action OnUpdateAvailable;

//    public void CheckForUpdate()
//    {
//        var isUpdateAvailable = // check logic, or signal from service worker
//        if (isUpdateAvailable)
//        {
//            OnUpdateAvailable?.Invoke();
//        }
//    }
//}


// Register the SignalR connection
builder.Services.AddScoped(_ =>
{
    var hubConnection = new HubConnectionBuilder()
        .WithUrl("https://localhost:7039/api/SignalR/send-message") // Replace with your actual hub URL
        .Build();

    return hubConnection;
});



await builder.Build().RunAsync();
