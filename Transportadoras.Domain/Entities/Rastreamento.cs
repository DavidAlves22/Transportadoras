using Transportadoras.Domain.Enums;
using Transportadoras.Domain.Exceptions;
using Transportadoras.Domain.ValueObjects;

namespace Transportadoras.Domain.Entities;

public sealed class Rastreamento
{
    private readonly List<EventoRastreio> _eventos = new();

    public string CodigoRastreio { get; private set; } = null!;
    public TransportadoraEnum Transportadora { get; private set; }
    public StatusRastreioEnum StatusAtual { get; private set; }
    public IReadOnlyCollection<EventoRastreio> Eventos => _eventos.AsReadOnly();

    public Rastreamento(string codigoRastreio, TransportadoraEnum transportadora, IEnumerable<EventoRastreio> eventos)
    {
        SetCodigoRastreio(codigoRastreio);
        SetTransportadora(transportadora);
        SetEventos(eventos);
    }

    private void SetCodigoRastreio(string codigoRastreio)
    {
        if (string.IsNullOrWhiteSpace(codigoRastreio))
            throw new CodigoRastreioInvalidoException("Código de rastreio não pode ser vazio.");

        CodigoRastreio = codigoRastreio.Trim().ToUpperInvariant();
    }

    private void SetTransportadora(TransportadoraEnum transportadora)
    {
        if (!Enum.IsDefined(transportadora))
            throw new TransportadoraNaoSuportadaException($"Transportadora '{transportadora}' é inválida.");

        Transportadora = transportadora;
    }

    private void SetEventos(IEnumerable<EventoRastreio> eventos)
    {
        ArgumentNullException.ThrowIfNull(eventos);

        var lista = eventos.ToList();

        if (lista.Count == 0)
            throw new ArgumentException("Rastreamento deve conter pelo menos um evento.");

        if (lista.Any(evento => evento is null))
            throw new ArgumentException("Rastreamento não pode conter evento nulo.");

        _eventos.AddRange(lista.OrderBy(evento => evento.DataHora));
        AtualizarStatusAtual();
    }

    public void AdicionarEvento(EventoRastreio evento)
    {
        ArgumentNullException.ThrowIfNull(evento);

        _eventos.Add(evento);
        _eventos.Sort((primeiro, segundo) => primeiro.DataHora.CompareTo(segundo.DataHora));
        AtualizarStatusAtual();
    }

    private void AtualizarStatusAtual() => StatusAtual = _eventos[^1].Status;
}
