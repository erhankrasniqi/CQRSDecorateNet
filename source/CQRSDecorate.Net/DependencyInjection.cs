using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using CQRSDecorate.Net.Abstractions;
using CQRSDecorate.Net.Dispatchers;

namespace CQRSDecorate.Net
{
    public static class DependencyInjection
    {
        public static void RegisterCqrsDecorator(this IServiceCollection services, params Type[] types)
        {
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();

            Assembly[] assemblies = types.Select(t => t.Assembly).ToArray();

            AutoRegisterHandlers(services, assemblies);
        }

        private static void AutoRegisterHandlers(IServiceCollection services, Assembly[] assemblies)
        {
            var handlerInterfaceTypes = new[]
            {
                typeof(ICommandHandler<,>),
                typeof(IQueryHandler<,>)
            };

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();

                foreach (var handlerType in handlerInterfaceTypes)
                {
                    var implementations = types
                        .Where(t => !t.IsAbstract && !t.IsInterface)
                        .SelectMany(t =>
                            t.GetInterfaces()
                             .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerType)
                             .Select(i => new { ServiceType = i, ImplementationType = t })
                        );

                    foreach (var impl in implementations)
                    {
                        services.AddScoped(impl.ServiceType, impl.ImplementationType);
                    }
                }
            }
        }
    }
}
