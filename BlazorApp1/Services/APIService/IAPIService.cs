using System.Threading.Tasks;

namespace BlazorApp1.Services.APIService
{
    public interface IAPIService
    {
        Task<bool> InvokeDelete(string uri);
        Task<T> InvokeGet<T>(string uri);
        Task<byte[]> InvokeGetPDF(string uri);
        Task<T> InvokePost<T>(string uri, T obj);
        Task InvokePut<T>(string uri, T obj);
        Task<T> InvokeGetAsync<T>(string uri);
        Task<T> InvokeRetPostAsync<T, U>(string uri, U obj);
    }
}