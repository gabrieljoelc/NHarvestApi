using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NHarvestApi
{
    public interface IResourceConverter
    {
        Task<T> Convert<T>(HttpResponseMessage response);
    }
}