using Transportadoras.Application.Interfaces;
using Transportadoras.Domain.Entities;
using Transportadoras.Domain.Enums;
using Transportadoras.Domain.Exceptions;
using Transportadoras.Infrastructure.Integracoes.Loggi.Mappers;

namespace Transportadoras.Infrastructure.Integracoes.Loggi;

public sealed class LoggiBuscaTrackingStrategy : IBuscaTrackingStrategy
{
    private readonly ILoggiClient _loggiClient;

    public LoggiBuscaTrackingStrategy(ILoggiClient loggiClient)
    {
        _loggiClient = loggiClient;
    }

    public TransportadoraEnum Transportadora => TransportadoraEnum.Loggi;

    public async Task<Rastreamento> BuscarTrackingAsync(string codigoRastreio, CancellationToken cancellationToken = default)
    {
        var contrato = await _loggiClient.ObterTrackingAsync(codigoRastreio, cancellationToken);

        if (contrato is null)
            throw new RastreamentoNaoEncontradoException($"Rastreamento '{codigoRastreio}' não encontrado na Loggi.");

        return contrato.ToDomain();
    }
}
