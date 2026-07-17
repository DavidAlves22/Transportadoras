namespace Transportadoras.Domain.Exceptions;

public sealed class CodigoRastreioInvalidoException : DomainException
{
    public CodigoRastreioInvalidoException(string mensagem) : base(mensagem)
    {
    }
}