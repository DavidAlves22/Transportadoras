using Transportadoras.Domain.Enums;

namespace Transportadoras.Application.DTOs.Tracking;

public sealed record BuscarTrackingResponse(
    string CodigoRastreio,
    TransportadoraEnum Transportadora,
    StatusRastreioEnum StatusAtual,
    IReadOnlyList<EventoRastreioResponse> Eventos);
