using System.Threading.Tasks;

namespace Common.ExternalFrameworks
{
    public interface ICacheFramework
    {
        Task<T> Get<T>(string key) where T : class;
        Task Save<T>(string key, T item);
    }
}