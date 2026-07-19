using System.Globalization;
using Transportadoras.Domain.Entities;
using Transportadoras.Domain.Enums;
using Transportadoras.Domain.ValueObjects;
using Transportadoras.Infrastructure.Integracoes.TotalExpress.Contracts;

namespace Transportadoras.Infrastructure.Integracoes.TotalExpress.Mappers;

public static class TotalExpressTrackingMapper
{
    private const string FormatoDataHora = "dd/MM/yyyy HH:mm";

    private static readonly Dictionary<int, StatusRastreioEnum> DeParaStatus = new()
    {
        [1] = StatusRastreioEnum.Postado,
        [2] = StatusRastreioEnum.EmTransito,
        [3] = StatusRastreioEnum.SaiuParaEntrega,
        [4] = StatusRastreioEnum.TentativaEntrega,
        [5] = StatusRastreioEnum.AguardandoRetirada,
        [6] = StatusRastreioEnum.Entregue,
        [7] = StatusRastreioEnum.Devolvido,
        [8] = StatusRastreioEnum.Extraviado,
        [9] = StatusRastreioEnum.Cancelado
    };

    public static Rastreamento ToDomain(this TotalExpressTrackingResponse contrato)
    {
        ArgumentNullException.ThrowIfNull(contrato);

        if (contrato.Ocorrencias is null || contrato.Ocorrencias.Count == 0)
            throw new ArgumentException("Resposta da TotalExpress não contém ocorrências de rastreio.");

        var eventos = contrato.Ocorrencias.Select(ToDomain).ToList();

        return new Rastreamento(contrato.CodigoObjeto!, TransportadoraEnum.TotalExpress, eventos);
    }

    private static EventoRastreio ToDomain(TotalExpressOcorrencia ocorrencia)
    {
        return new EventoRastreio(
            ConverterDataHora(ocorrencia.DataHora),
            ConverterStatus(ocorrencia.CodStatus),
            ocorrencia.Situacao ?? "Sem descrição",
            ocorrencia.Praca);
    }

    private static DateTime ConverterDataHora(string? dataHora)
    {
        if (DateTime.TryParseExact(dataHora, FormatoDataHora, CultureInfo.InvariantCulture, DateTimeStyles.None, out var convertida))
            return convertida;

        throw new ArgumentException($"Data/hora '{dataHora}' da TotalExpress não está no formato '{FormatoDataHora}'.");
    }

    private static StatusRastreioEnum ConverterStatus(int codStatus)
    {
        if (DeParaStatus.TryGetValue(codStatus, out var convertido))
            return convertido;

        throw new ArgumentException($"Status '{codStatus}' da TotalExpress não é reconhecido.");
    }
}
