using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Services.LocalStorageServices
{
    public class LocalStorageServices
    {

        private IJSRuntime JSRuntime { get; set; }

        public LocalStorageServices(IJSRuntime jSRuntime) 
        {
            JSRuntime = jSRuntime;
        }

        public async Task SetItemAsync(string key, string value) 
        {
            await JSRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
        }

        public async Task<string> GetItemAsync(string key) 
        {
         var result =  await JSRuntime.InvokeAsync<string>("localStorage.getItem", key);

            return result;
        }


    }
}
