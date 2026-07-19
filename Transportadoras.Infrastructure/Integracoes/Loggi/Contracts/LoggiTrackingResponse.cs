using System.Text.Json.Serialization;

namespace Transportadoras.Infrastructure.Integracoes.Loggi.Contracts;

public sealed class LoggiTrackingResponse
{
    [JsonPropertyName("trackingCode")]
    public string? TrackingCode { get; set; }

    [JsonPropertyName("events")]
    public List<LoggiTrackingEvent>? Events { get; set; }
}

public sealed class LoggiTrackingEvent
{
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTimeOffset? Timestamp { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }
}
