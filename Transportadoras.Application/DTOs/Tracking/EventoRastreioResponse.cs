using Transportadoras.Domain.Enums;

namespace Transportadoras.Application.DTOs.Tracking;

public sealed record EventoRastreioResponse(
    DateTime DataHora,
    StatusRastreioEnum Status,
    string Descricao,
    string? Localizacao);
