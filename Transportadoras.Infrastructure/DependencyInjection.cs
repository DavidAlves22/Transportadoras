using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Transportadoras.Application.Interfaces;
using Transportadoras.Infrastructure.Configuracoes;
using Transportadoras.Infrastructure.Integracoes.Loggi;
using Transportadoras.Infrastructure.Integracoes.TotalExpress;
using Transportadoras.Infrastructure.Simulacao;

namespace Transportadoras.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var secao = configuration.GetSection(TransportadoraApiOptions.SecaoConfiguracao);
        services.Configure<TransportadoraApiOptions>(secao);

        var options = secao.Get<TransportadoraApiOptions>() ?? new TransportadoraApiOptions();

        var loggi = services.AddHttpClient<ILoggiClient, LoggiClient>(client =>
            client.BaseAddress = ResolverBaseUrl(options.Loggi.BaseUrl, nameof(options.Loggi)));

        var totalExpress = services.AddHttpClient<ITotalExpressClient, TotalExpressClient>(client =>
            client.BaseAddress = ResolverBaseUrl(options.TotalExpress.BaseUrl, nameof(options.TotalExpress)));

        if (options.ModoSimulado)
        {
            loggi.ConfigurePrimaryHttpMessageHandler(() => new StubHttpMessageHandler(RespostasSimuladas.Loggi));
            totalExpress.ConfigurePrimaryHttpMessageHandler(() => new StubHttpMessageHandler(RespostasSimuladas.TotalExpress));
        }

        services.AddScoped<IBuscaTrackingStrategy, LoggiBuscaTrackingStrategy>();
        services.AddScoped<IBuscaTrackingStrategy, TotalExpressBuscaTrackingStrategy>();

        return services;
    }

    private static Uri ResolverBaseUrl(string baseUrl, string nomeTransportadora)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
            throw new InvalidOperationException($"BaseUrl da transportadora '{nomeTransportadora}' não configurada na seção '{TransportadoraApiOptions.SecaoConfiguracao}'.");

        return new Uri(baseUrl.EndsWith('/') ? baseUrl : baseUrl + "/");
    }
}
