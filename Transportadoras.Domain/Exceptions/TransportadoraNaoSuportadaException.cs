namespace Transportadoras.Domain.Exceptions;

public sealed class TransportadoraNaoSuportadaException : DomainException
{
    public TransportadoraNaoSuportadaException(string mensagem) : base(mensagem)
    {
    }
}