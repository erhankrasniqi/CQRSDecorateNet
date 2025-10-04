using System.Threading.Tasks;

namespace CQRSDecorate.Net.Abstractions
{
    public interface IQueryDispatcher
    {
        Task<TResult> SendAsync<TResult>(IQuery<TResult> query);
    }
}
