using System;
using System.Threading.Tasks;
using CQRSDecorate.Net.Abstractions;

namespace CQRSDecorate.Net.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> SendAsync<TResult>(ICommand<TResult> command)
        {
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
            var handler = _serviceProvider.GetService(handlerType);

            return handler == null
                ? throw new InvalidOperationException($"No handler found for {command.GetType().Name}")
                : await (Task<TResult>)handlerType.GetMethod("HandleAsync")!.Invoke(handler, new object[] { command })!;
        }
    }
}
