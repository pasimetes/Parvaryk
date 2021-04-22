using System;
using System.Threading.Tasks;

namespace Parvaryk.Application.Common.Interfaces
{
    public interface IMemoryCacheProvider
    {
        Task<TResult> Get<TResult>(string key, Func<Task<TResult>> retrievalOperation);

        void Invalidate(string key);
    }
}
