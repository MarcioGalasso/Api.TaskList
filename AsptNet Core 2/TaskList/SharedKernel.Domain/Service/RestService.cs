using Newtonsoft.Json;
using SharedKernel.Domain.Services.Interface;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Domain.Services
{
    public class RestService : IRestService
    {
        private void SetHeaders(HttpClient poClient, Dictionary<string, string> poHeaders)
        {
            if (poHeaders == null)
                return;

            foreach (KeyValuePair<string, string> header in poHeaders)
            {
                poClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        public async Task<T> DeleteAsync<T>(string psApi, string psMethod, int piId, Dictionary<string, string> poHeaders = null) where T : class
        {
            string sResult = await DeleteAsync(psApi, psMethod, piId, poHeaders);
            T oObj = JsonConvert.DeserializeObject<T>(sResult);
            return oObj;
        }

        public async Task<string> DeleteAsync(string psApi, string psMethod, int piId, Dictionary<string, string> poHeaders = null)
        {
            using (HttpClient oClient = new HttpClient())
            {
                oClient.DefaultRequestHeaders.Accept.Clear();
                oClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                this.SetHeaders(oClient, poHeaders);
                HttpResponseMessage oResponse = await oClient.DeleteAsync($"{psApi}{psMethod}/{piId}");
                string sResult = oResponse.Content.ReadAsStringAsync().Result;
                return sResult;
            }
        }

        public async Task<T> GetAsync<T>(string psApi, string psMethod, Dictionary<string, string> poHeaders = null) where T : class
        {
            string sResult = await GetAsync(psApi, psMethod, poHeaders);
            T oObj = JsonConvert.DeserializeObject<T>(sResult);
            return oObj;
        }

        public async Task<string> GetAsync(string psApi, string psMethod, Dictionary<string, string> poHeaders = null)
        {
            using (HttpClient oClient = new HttpClient())
            {
                oClient.DefaultRequestHeaders.Accept.Clear();
                oClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                this.SetHeaders(oClient, poHeaders);
                HttpResponseMessage oResponse = await oClient.GetAsync($"{psApi}{psMethod}");
                return oResponse.Content.ReadAsStringAsync().Result;
            }
        }

        public async Task<string> PostAsync<T>(string psApi, string psMethod, T poObj, Dictionary<string, string> poHeaders = null) where T : class
        {
            using (HttpClient oClient = new HttpClient())
            {
                oClient.DefaultRequestHeaders.Accept.Clear();
                oClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                this.SetHeaders(oClient, poHeaders);
                string sJson = JsonConvert.SerializeObject(poObj);
                HttpContent oContent = new StringContent(sJson, Encoding.UTF8, "application/json");
                HttpResponseMessage oResponse = await oClient.PostAsync($"{psApi}{psMethod}", oContent);
                return oResponse.Content.ReadAsStringAsync().Result;
            }
        }

        public async Task<T> PostAsync<T, C>(string psApi, string psMethod, C poObj, Dictionary<string, string> poHeaders = null)
            where T : class
            where C : class
        {
            string sResult = await PostAsync<C>(psApi, psMethod, poObj, poHeaders);
            T oObj = JsonConvert.DeserializeObject<T>(sResult);
            return oObj;
        }
    }
}
