using System;
using System.Threading.Tasks;
using CQRSDecorate.Net.Abstractions;

namespace CQRSDecorate.Net.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> SendAsync<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            var handler = _serviceProvider.GetService(handlerType) ?? throw new InvalidOperationException($"No handler found for query {query.GetType().Name}");

            return await (Task<TResult>)handlerType
                .GetMethod("HandleAsync")!
                .Invoke(handler, new object[] { query })!;
        }
    }
}
