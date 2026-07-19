using Transportadoras.Domain.Enums;
using Transportadoras.Infrastructure.Integracoes.Loggi.Contracts;

namespace Transportadoras.Infrastructure.Integracoes.Loggi;

public sealed class LoggiClient : TransportadoraApiClientBase, ILoggiClient
{
    public LoggiClient(HttpClient httpClient) : base(httpClient)
    {
    }

    protected override TransportadoraEnum Transportadora => TransportadoraEnum.Loggi;

    public Task<LoggiTrackingResponse?> ObterTrackingAsync(string codigoRastreio, CancellationToken cancellationToken = default) => ObterAsync<LoggiTrackingResponse>($"tracking/{codigoRastreio}", cancellationToken);
}
