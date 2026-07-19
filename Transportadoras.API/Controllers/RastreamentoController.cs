using Microsoft.AspNetCore.Mvc;
using Transportadoras.Application.DTOs.Tracking;
using Transportadoras.Application.UseCases;
using Transportadoras.Domain.Enums;

namespace Transportadoras.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class RastreamentoController : ControllerBase
{
    private readonly BuscarTrackingUseCase _buscarTrackingUseCase;

    public RastreamentoController(BuscarTrackingUseCase buscarTrackingUseCase)
    {
        _buscarTrackingUseCase = buscarTrackingUseCase;
    }

    /// <summary>
    /// Busca o rastreamento de um objeto em uma transportadora.
    /// </summary>
    [HttpGet("{transportadora}/{codigoRastreio}")]
    [ProducesResponseType(typeof(BuscarTrackingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public async Task<ActionResult<BuscarTrackingResponse>> BuscarTracking(TransportadoraEnum transportadora, string codigoRastreio, CancellationToken cancellationToken)
    {
        var request = new BuscarTrackingRequest(transportadora, codigoRastreio);
        var resposta = await _buscarTrackingUseCase.ExecutarAsync(request, cancellationToken);

        return Ok(resposta);
    }
}
