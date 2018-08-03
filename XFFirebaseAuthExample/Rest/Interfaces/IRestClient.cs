using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace elloore.Rest.Interfaces
{
    public interface IRestClient
    {
        Task<TResult> MakeApiCall<TResult>(string url, HttpMethod method, bool loginRequired = true, object data = null, string contentType = "application/json")
                        where TResult : class;
    }
}
