
using Microsoft.JSInterop;

namespace BlazorApp1.Services.AppUpdateService
{
    public class AppUpdateService
    {
        private readonly IJSRuntime _jsRuntime;

        public event Action OnUpdateAvailable;


        public AppUpdateService( IJSRuntime jSRuntime)
        {
           _jsRuntime = jSRuntime;
        }

        public void Initialize()
        {
            _jsRuntime.InvokeVoidAsync("navigator.serviceWorker.ready")
                .AsTask()
                .ContinueWith(_ => StartListeningForUpdates());
        }
        private void StartListeningForUpdates()
        {
            _jsRuntime.InvokeVoidAsync("blazorApp.listenForUpdates", DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        public void NotifyUpdateAvailable()
        {
            OnUpdateAvailable?.Invoke();
        }
    }
}
