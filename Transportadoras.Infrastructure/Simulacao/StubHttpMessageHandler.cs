using System.Net;
using System.Text;

namespace Transportadoras.Infrastructure.Simulacao;

public sealed class StubHttpMessageHandler : HttpMessageHandler
{
    private readonly IReadOnlyDictionary<string, string> _respostasPorCodigo;

    public StubHttpMessageHandler(IReadOnlyDictionary<string, string> respostasPorCodigo)
    {
        _respostasPorCodigo = respostasPorCodigo;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var codigoRastreio = request.RequestUri!.Segments[^1].Trim('/');

        if (!_respostasPorCodigo.TryGetValue(codigoRastreio, out var json))
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound) { RequestMessage = request });

        var resposta = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json"),
            RequestMessage = request
        };

        return Task.FromResult(resposta);
    }
}
