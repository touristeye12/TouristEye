using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace elloore.Rest.Interfaces
{
    public interface IRestService
    {
        Task<TResult> RefreshDataAsync<TResult>(string url) where TResult : class;

        Task<TResult> SaveAsync<TData, TResult>(string url, TData item, bool isNewItem);

        Task<TResult> SaveAsync<TResult>(string url);

        Task<bool> DeleteAsync(string url);
    }
}
