using System.Threading.Tasks;

namespace CQRSDecorate.Net.Abstractions
{
    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}
