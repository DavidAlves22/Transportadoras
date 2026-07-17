using Transportadoras.Application.Interfaces;
using Transportadoras.Domain.Enums;
using Transportadoras.Domain.Exceptions;

namespace Transportadoras.Application.Resolvers;

public sealed class TransportadoraStrategyResolver : ITransportadoraStrategyResolver
{
    private readonly IReadOnlyDictionary<TransportadoraEnum, IBuscaTrackingStrategy> _buscaTrackingStrategies;

    public TransportadoraStrategyResolver(IEnumerable<IBuscaTrackingStrategy> buscaTrackingStrategies)
    {
        ArgumentNullException.ThrowIfNull(buscaTrackingStrategies);

        _buscaTrackingStrategies = buscaTrackingStrategies.ToDictionary(strategy => strategy.Transportadora);
    }

    public IBuscaTrackingStrategy ResolverBuscaTracking(TransportadoraEnum transportadora)
    {
        if (!_buscaTrackingStrategies.TryGetValue(transportadora, out var strategy))
            throw new TransportadoraNaoSuportadaException($"Transportadora '{transportadora}' não suporta busca de tracking.");

        return strategy;
    }
}
