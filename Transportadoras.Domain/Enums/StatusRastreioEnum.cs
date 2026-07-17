namespace Transportadoras.Domain.Enums;

public enum StatusRastreioEnum
{
    Postado = 1,
    EmTransito = 2,
    SaiuParaEntrega = 3,
    TentativaEntrega = 4,
    AguardandoRetirada = 5,
    Entregue = 6,
    Devolvido = 7,
    Extraviado = 8,
    Cancelado = 9
}