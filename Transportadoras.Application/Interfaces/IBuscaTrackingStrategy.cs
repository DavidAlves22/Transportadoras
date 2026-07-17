using Transportadoras.Domain.Entities;
using Transportadoras.Domain.Enums;

namespace Transportadoras.Application.Interfaces;

public interface IBuscaTrackingStrategy
{
    TransportadoraEnum Transportadora { get; }

    Task<Rastreamento> BuscarTrackingAsync(string codigoRastreio, CancellationToken cancellationToken = default);
}
