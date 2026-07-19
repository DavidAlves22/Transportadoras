namespace Transportadoras.Infrastructure.Configuracoes;

public sealed class TransportadoraApiOptions
{
    public const string SecaoConfiguracao = "Transportadoras";

    public bool ModoSimulado { get; set; } = true;

    public TransportadoraApiEndpoint Loggi { get; set; } = new();

    public TransportadoraApiEndpoint TotalExpress { get; set; } = new();
}

public sealed class TransportadoraApiEndpoint
{
    public string BaseUrl { get; set; } = string.Empty;

    public string? ApiKey { get; set; }
}
