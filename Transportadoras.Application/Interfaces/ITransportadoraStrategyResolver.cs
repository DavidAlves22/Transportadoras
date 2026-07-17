using Transportadoras.Domain.Enums;

namespace Transportadoras.Application.Interfaces;

public interface ITransportadoraStrategyResolver
{
    IBuscaTrackingStrategy ResolverBuscaTracking(TransportadoraEnum transportadora);
}
