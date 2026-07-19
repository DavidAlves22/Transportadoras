using Transportadoras.Infrastructure.Integracoes.TotalExpress.Contracts;

namespace Transportadoras.Infrastructure.Integracoes.TotalExpress;

public interface ITotalExpressClient
{
    Task<TotalExpressTrackingResponse?> ObterTrackingAsync(string codigoRastreio, CancellationToken cancellationToken = default);
}
