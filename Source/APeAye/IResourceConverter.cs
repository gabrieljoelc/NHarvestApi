using System.Net.Http;
using System.Threading.Tasks;

namespace APeAye
{
    public interface IResourceConverter
    {
        Task<T> Convert<T>(HttpResponseMessage response);
    }
}