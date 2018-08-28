using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharedKernel.Domain.Services.Interface
{
    public interface IRestService
    {
        Task<T> GetAsync<T>(string psApi, string psMethod, Dictionary<string, string> poHeaders = null) where T : class;
        Task<string> GetAsync(string psApi, string psMethod, Dictionary<string, string> poHeaders = null);
        Task<string> PostAsync<T>(string psApi, string psMethod, T poObj, Dictionary<string, string> poHeaders = null) where T : class;
        Task<T> PostAsync<T, C>(string psApi, string psMethod, C poObj, Dictionary<string, string> poHeaders = null) where T : class where C : class;
        Task<T> DeleteAsync<T>(string psApi, string psMethod, int piId, Dictionary<string, string> poHeaders = null) where T : class;
        Task<string> DeleteAsync(string psApi, string psMethod, int piId, Dictionary<string, string> poHeaders = null);
    }
}
