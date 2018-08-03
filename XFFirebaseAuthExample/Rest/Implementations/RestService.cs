using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using elloore.Exceptions;
using elloore.Models;

namespace elloore.Rest.Interfaces
{
    public class RestService : IRestService
    {

        HttpClient client;

        public RestService(bool authenticationRequired = true)
        {
            client = new HttpClient { MaxResponseContentBufferSize = 256000 };
            if (authenticationRequired)
            {
                if (CacheData.User != null && !String.IsNullOrEmpty(CacheData.User.token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CacheData.User.token);
                }
                else
                    throw new UnAuthorizedException();
            }
        }


        public async Task<TResult> RefreshDataAsync<TResult>(string url) where TResult : class
        {
            TResult result = null;
            var uri = new Uri(url);

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<TResult>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return result;
        }

        public async Task<TResult> SaveAsync<TData, TResult>(string url, TData data, bool isNewItem)
        {
            var uri = new Uri(url);
            TResult result = default(TResult);

            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    string jResult = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<TResult>(jResult);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return result;
        }

        public async Task<TResult> SaveAsync<TResult>(string url)
        {
            var uri = new Uri(url);
            TResult result = default(TResult);

            try
            {

                var json = JsonConvert.SerializeObject(String.Empty);
                var content = new StringContent("", Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await client.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string jResult = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<TResult>(jResult);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return result;
        }

        public async Task<bool> DeleteAsync(string url)
        {
            var uri = new Uri(url);
            bool success = false;
            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    success = true;
                    Debug.WriteLine(@"				TodoItem successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return success;
        }

    }
}
