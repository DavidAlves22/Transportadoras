using Transportadoras.Domain.Enums;
using Transportadoras.Infrastructure.Integracoes.TotalExpress.Contracts;

namespace Transportadoras.Infrastructure.Integracoes.TotalExpress;

public sealed class TotalExpressClient : TransportadoraApiClientBase, ITotalExpressClient
{
    public TotalExpressClient(HttpClient httpClient) : base(httpClient)
    {
    }

    protected override TransportadoraEnum Transportadora => TransportadoraEnum.TotalExpress;

    public Task<TotalExpressTrackingResponse?> ObterTrackingAsync(string codigoRastreio, CancellationToken cancellationToken = default) => ObterAsync<TotalExpressTrackingResponse>($"rastreio/{codigoRastreio}", cancellationToken);
}
