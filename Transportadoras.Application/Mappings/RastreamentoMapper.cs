using Transportadoras.Application.DTOs.Tracking;
using Transportadoras.Domain.Entities;
using Transportadoras.Domain.ValueObjects;

namespace Transportadoras.Application.Mappings;

public static class RastreamentoMapper
{
    public static BuscarTrackingResponse ToResponse(this Rastreamento rastreamento)
    {
        ArgumentNullException.ThrowIfNull(rastreamento);

        return new BuscarTrackingResponse(
            rastreamento.CodigoRastreio,
            rastreamento.Transportadora,
            rastreamento.StatusAtual,
            rastreamento.Eventos.Select(evento => evento.ToResponse()).ToList());
    }

    public static EventoRastreioResponse ToResponse(this EventoRastreio evento) =>
        new(evento.DataHora, evento.Status, evento.Descricao, evento.Localizacao);
}
