using System.Reflection;

namespace RoomHub.Api.Common.CQRS;

public static class HandlerRegistrationExtensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services, Assembly assembly)
    {
        var implementations = assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false });

        foreach (var impl in implementations)
        {
            var interfaces = impl.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>));

            foreach (var iface in interfaces)
            {
                services.AddScoped(iface, impl);
            }
        }
        return services;
    }
}