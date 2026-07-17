using Transportadoras.Application.DTOs.Tracking;
using Transportadoras.Application.Interfaces;
using Transportadoras.Application.Mappings;

namespace Transportadoras.Application.UseCases;

public sealed class BuscarTrackingUseCase
{
    private readonly ITransportadoraStrategyResolver _resolver;

    public BuscarTrackingUseCase(ITransportadoraStrategyResolver resolver)
    {
        _resolver = resolver;
    }

    public async Task<BuscarTrackingResponse> ExecutarAsync(BuscarTrackingRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var strategy = _resolver.ResolverBuscaTracking(request.Transportadora);
        var rastreamento = await strategy.BuscarTrackingAsync(request.CodigoRastreio, cancellationToken);

        return rastreamento.ToResponse();
    }
}
