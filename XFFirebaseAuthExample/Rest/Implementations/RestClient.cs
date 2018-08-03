using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using elloore.Exceptions;
using elloore.Models;

namespace elloore.Rest.Interfaces
{
    public class RestClient : IRestClient
    {

        public RestClient()
        {
        }

        public async Task<TResult> MakeApiCall<TResult>(string url, HttpMethod method, bool loginRequired = true, object data = null, string contentType = "application/json") where TResult : class
        {
            //url = url.Replace("http://", "https://");

            using (var httpClient = new HttpClient(new NativeMessageHandler()))
            {
                if (loginRequired)
                {
                    if (CacheData.User != null && !String.IsNullOrEmpty(CacheData.User.token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CacheData.User.token);
                    }
                    else
                        throw new UnAuthorizedException();
                }


                using (var request = new HttpRequestMessage { RequestUri = new Uri(url), Method = method })
                {

                    // add content
                    if (method != HttpMethod.Get)
                    {
                        var json = JsonConvert.SerializeObject(data);
                        request.Content = new StringContent(json, Encoding.UTF8, contentType);
                    }


                    HttpResponseMessage response = new HttpResponseMessage();
                    try
                    {
                        response = await httpClient.SendAsync(request).ConfigureAwait(false);
                        response.EnsureSuccessStatusCode();
                        var stringSerialized = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        if (stringSerialized.StartsWith("-"))
                            stringSerialized = stringSerialized.Remove(0, 1);
                        // deserialize content
                        return JsonConvert.DeserializeObject<TResult>(stringSerialized);
                    }
                    catch (Exception exp)
                    {
                        var stringSerialized = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        // log error
                    }
                    return null;
                }
            }
        }

    }
}
