using System.Threading.Tasks;

namespace CQRSDecorate.Net.Abstractions
{
    public interface ICommandDispatcher
    {
        Task<TResult> SendAsync<TResult>(ICommand<TResult> command);
    }
}
