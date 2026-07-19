using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Transportadoras.Application.Exceptions;
using Transportadoras.Domain.Enums;

namespace Transportadoras.Infrastructure.Integracoes;

public abstract class TransportadoraApiClientBase
{
    private readonly HttpClient _httpClient;

    protected TransportadoraApiClientBase(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    protected abstract TransportadoraEnum Transportadora { get; }

    protected async Task<TContrato?> ObterAsync<TContrato>(string rota, CancellationToken cancellationToken)
        where TContrato : class
    {
        try
        {
            using var resposta = await _httpClient.GetAsync(rota, cancellationToken);

            if (resposta.StatusCode == HttpStatusCode.NotFound)
                return null;

            resposta.EnsureSuccessStatusCode();

            return await resposta.Content.ReadFromJsonAsync<TContrato>(cancellationToken);
        }
        catch (HttpRequestException excecao)
        {
            throw new TransportadoraIndisponivelException(
                Transportadora,
                $"Falha ao consultar a API da transportadora '{Transportadora}'.",
                excecao);
        }
        catch (TaskCanceledException excecao) when (!cancellationToken.IsCancellationRequested)
        {
            throw new TransportadoraIndisponivelException(
                Transportadora,
                $"Tempo limite excedido ao consultar a API da transportadora '{Transportadora}'.",
                excecao);
        }
        catch (JsonException excecao)
        {
            throw new TransportadoraIndisponivelException(
                Transportadora,
                $"A API da transportadora '{Transportadora}' retornou uma resposta ilegível.",
                excecao);
        }
    }
}
