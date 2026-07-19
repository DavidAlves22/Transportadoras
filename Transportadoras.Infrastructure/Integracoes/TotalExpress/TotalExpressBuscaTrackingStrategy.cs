using Transportadoras.Application.Interfaces;
using Transportadoras.Domain.Entities;
using Transportadoras.Domain.Enums;
using Transportadoras.Domain.Exceptions;
using Transportadoras.Infrastructure.Integracoes.TotalExpress.Mappers;

namespace Transportadoras.Infrastructure.Integracoes.TotalExpress;

public sealed class TotalExpressBuscaTrackingStrategy : IBuscaTrackingStrategy
{
    private readonly ITotalExpressClient _totalExpressClient;

    public TotalExpressBuscaTrackingStrategy(ITotalExpressClient totalExpressClient)
    {
        _totalExpressClient = totalExpressClient;
    }

    public TransportadoraEnum Transportadora => TransportadoraEnum.TotalExpress;

    public async Task<Rastreamento> BuscarTrackingAsync(string codigoRastreio, CancellationToken cancellationToken = default)
    {
        var contrato = await _totalExpressClient.ObterTrackingAsync(codigoRastreio, cancellationToken);

        if (contrato is null)
            throw new RastreamentoNaoEncontradoException($"Rastreamento '{codigoRastreio}' não encontrado na TotalExpress.");

        return contrato.ToDomain();
    }
}
