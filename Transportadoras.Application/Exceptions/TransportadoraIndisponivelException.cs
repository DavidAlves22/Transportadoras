using Transportadoras.Domain.Enums;

namespace Transportadoras.Application.Exceptions;

public sealed class TransportadoraIndisponivelException : Exception
{
    public TransportadoraEnum Transportadora { get; }

    public TransportadoraIndisponivelException(
        TransportadoraEnum transportadora,
        string mensagem,
        Exception excecaoInterna)
        : base(mensagem, excecaoInterna)
    {
        Transportadora = transportadora;
    }
}
