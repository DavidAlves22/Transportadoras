using Transportadoras.Domain.Entities;
using Transportadoras.Domain.Enums;
using Transportadoras.Domain.ValueObjects;
using Transportadoras.Infrastructure.Integracoes.Loggi.Contracts;

namespace Transportadoras.Infrastructure.Integracoes.Loggi.Mappers;

public static class LoggiTrackingMapper
{
    private static readonly Dictionary<string, StatusRastreioEnum> DeParaStatus = new(StringComparer.OrdinalIgnoreCase)
    {
        ["posted"] = StatusRastreioEnum.Postado,
        ["in_transit"] = StatusRastreioEnum.EmTransito,
        ["out_for_delivery"] = StatusRastreioEnum.SaiuParaEntrega,
        ["delivery_attempt"] = StatusRastreioEnum.TentativaEntrega,
        ["awaiting_pickup"] = StatusRastreioEnum.AguardandoRetirada,
        ["delivered"] = StatusRastreioEnum.Entregue,
        ["returned"] = StatusRastreioEnum.Devolvido,
        ["lost"] = StatusRastreioEnum.Extraviado,
        ["cancelled"] = StatusRastreioEnum.Cancelado
    };

    public static Rastreamento ToDomain(this LoggiTrackingResponse contrato)
    {
        ArgumentNullException.ThrowIfNull(contrato);

        if (contrato.Events is null || contrato.Events.Count == 0)
            throw new ArgumentException("Resposta da Loggi não contém eventos de rastreio.");

        var eventos = contrato.Events.Select(ToDomain).ToList();

        return new Rastreamento(contrato.TrackingCode!, TransportadoraEnum.Loggi, eventos);
    }

    private static EventoRastreio ToDomain(LoggiTrackingEvent evento)
    {
        if (evento.Timestamp is null)
            throw new ArgumentException("Evento da Loggi não possui 'timestamp'.");

        return new EventoRastreio(evento.Timestamp.Value.UtcDateTime,
                                  ConverterStatus(evento.Status),
                                  evento.Description ?? evento.Status ?? "Sem descrição",
                                  evento.City);
    }

    private static StatusRastreioEnum ConverterStatus(string? status)
    {
        if (status is not null && DeParaStatus.TryGetValue(status, out var convertido))
            return convertido;

        throw new ArgumentException($"Status '{status}' da Loggi não é reconhecido.");
    }
}
