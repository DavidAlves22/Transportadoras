using System.Text.Json.Serialization;

namespace Transportadoras.Infrastructure.Integracoes.TotalExpress.Contracts;

public sealed class TotalExpressTrackingResponse
{
    [JsonPropertyName("codigoObjeto")]
    public string? CodigoObjeto { get; set; }

    [JsonPropertyName("ocorrencias")]
    public List<TotalExpressOcorrencia>? Ocorrencias { get; set; }
}

public sealed class TotalExpressOcorrencia
{
    [JsonPropertyName("codStatus")]
    public int CodStatus { get; set; }

    [JsonPropertyName("situacao")]
    public string? Situacao { get; set; }

    [JsonPropertyName("dataHora")]
    public string? DataHora { get; set; }

    [JsonPropertyName("praca")]
    public string? Praca { get; set; }
}
