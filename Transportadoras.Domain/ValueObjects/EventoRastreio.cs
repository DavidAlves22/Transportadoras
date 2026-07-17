using Transportadoras.Domain.Enums;

namespace Transportadoras.Domain.ValueObjects;

public sealed class EventoRastreio : IEquatable<EventoRastreio>
{
    public DateTime DataHora { get; private set; }
    public StatusRastreioEnum Status { get; private set; }
    public string Descricao { get; private set; } = null!;
    public string? Localizacao { get; private set; }

    public EventoRastreio(DateTime dataHora, StatusRastreioEnum status, string descricao, string? localizacao = null)
    {
        SetDataHora(dataHora);
        SetStatus(status);
        SetDescricao(descricao);
        SetLocalizacao(localizacao);
    }

    private void SetDataHora(DateTime dataHora)
    {
        if (dataHora == default)
            throw new ArgumentException("Data/hora do evento de rastreio não pode ser vazia.");

        DataHora = dataHora;
    }

    private void SetStatus(StatusRastreioEnum status)
    {
        if (!Enum.IsDefined(status))
            throw new ArgumentException($"Status de rastreio '{status}' é inválido.");

        Status = status;
    }

    private void SetDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição do evento de rastreio não pode ser vazia.");

        Descricao = descricao.Trim();
    }

    private void SetLocalizacao(string? localizacao)
    {
        Localizacao = string.IsNullOrWhiteSpace(localizacao) ? null : localizacao.Trim();
    }

    public bool Equals(EventoRastreio? other)
    {
        if (other is null)
            return false;

        return DataHora == other.DataHora
            && Status == other.Status
            && Descricao == other.Descricao
            && Localizacao == other.Localizacao;
    }

    public override bool Equals(object? obj) => Equals(obj as EventoRastreio);

    public override int GetHashCode() => HashCode.Combine(DataHora, Status, Descricao, Localizacao);
}