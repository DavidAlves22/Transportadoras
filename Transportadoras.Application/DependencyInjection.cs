using Microsoft.Extensions.DependencyInjection;
using Transportadoras.Application.Interfaces;
using Transportadoras.Application.Resolvers;
using Transportadoras.Application.UseCases;

namespace Transportadoras.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITransportadoraStrategyResolver, TransportadoraStrategyResolver>();
        services.AddScoped<BuscarTrackingUseCase>();

        return services;
    }
}
