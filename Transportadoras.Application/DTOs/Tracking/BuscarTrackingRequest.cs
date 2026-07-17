using Transportadoras.Domain.Enums;

namespace Transportadoras.Application.DTOs.Tracking;

public sealed record BuscarTrackingRequest(TransportadoraEnum Transportadora, string CodigoRastreio);
