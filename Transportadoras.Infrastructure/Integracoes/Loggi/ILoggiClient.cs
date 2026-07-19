using Transportadoras.Infrastructure.Integracoes.Loggi.Contracts;

namespace Transportadoras.Infrastructure.Integracoes.Loggi;

public interface ILoggiClient
{
    Task<LoggiTrackingResponse?> ObterTrackingAsync(string codigoRastreio, CancellationToken cancellationToken = default);
}
