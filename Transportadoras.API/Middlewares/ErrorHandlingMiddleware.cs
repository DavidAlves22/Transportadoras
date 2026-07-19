using System.Net;
using System.Text.Json;
using Transportadoras.Application.Exceptions;
using Transportadoras.Domain.Exceptions;

namespace Transportadoras.API.Middlewares;

public sealed class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception excecao)
        {
            await TratarExcecaoAsync(context, excecao);
        }
    }

    private async Task TratarExcecaoAsync(HttpContext context, Exception excecao)
    {
        var (statusCode, mensagem) = MapearExcecao(excecao);

        if (statusCode >= HttpStatusCode.InternalServerError)
            _logger.LogError(excecao, "Falha ao processar {Metodo} {Caminho}.", context.Request.Method, context.Request.Path);
        else
            _logger.LogWarning("Requisição {Metodo} {Caminho} rejeitada ({StatusCode}): {Mensagem}", context.Request.Method, context.Request.Path, (int)statusCode, mensagem);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var resultado = JsonSerializer.Serialize(new { erro = mensagem });
        await context.Response.WriteAsync(resultado);
    }

    private static (HttpStatusCode StatusCode, string Mensagem) MapearExcecao(Exception excecao) => excecao switch
    {
        RastreamentoNaoEncontradoException naoEncontrado => (HttpStatusCode.NotFound, naoEncontrado.Message),

        TransportadoraIndisponivelException indisponivel => (HttpStatusCode.BadGateway, indisponivel.Message),

        DomainException dominio => (HttpStatusCode.BadRequest, dominio.Message),

        ArgumentException argumento => (HttpStatusCode.BadRequest, argumento.Message),

        _ => (HttpStatusCode.InternalServerError, "Ocorreu um erro inesperado ao processar a requisição.")
    };
}

public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder) => builder.UseMiddleware<ErrorHandlingMiddleware>();
}
