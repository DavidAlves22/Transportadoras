namespace Transportadoras.Domain.Exceptions;

public sealed class RastreamentoNaoEncontradoException : DomainException
{
    public RastreamentoNaoEncontradoException(string mensagem) : base(mensagem)
    {
    }
}