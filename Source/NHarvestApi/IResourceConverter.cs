using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NHarvestApi
{
    public interface IResourceConverter
    {
        Task<T> Get<T>(HttpClient httpClient, string resourceRelativePath);
    }
}