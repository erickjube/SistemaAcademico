using Microsoft.AspNetCore.Mvc;
using SistemaAcademico.DTOs.NotaDto;
using SistemaAcademico.Services;
using SistemaAcademico.Services.Interfaces;

namespace SistemaAcademico.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiarioController : ControllerBase
{
    private readonly INotaService _notaService;

    public DiarioController(INotaService notaService)
    {
        _notaService = notaService;
    }

    [HttpGet("turma/{turmaId}")]
    public async Task<ActionResult> BuscarDiario(int turmaId)
    {
        var diario = await _notaService.BuscarDiarioAsync(turmaId);
        return Ok(diario);
    }

    [HttpPost("notas")]
    public async Task<ActionResult> LancarNota(CriarNotaDto dto)
    {
        await _notaService.LancarNotaAsync(dto);
        return Ok();
    }

    [HttpPut("notas")]
    public async Task<ActionResult> AtualizarNota(CriarNotaDto dto)
    {
        await _notaService.AtualizarNotaAsync(dto);
        return Ok();
    }


    [HttpPost("{turmaId}/fechar")]
    public async Task<ActionResult> FecharDiario(int turmaId)
    {
        await _notaService.FecharDiarioAsync(turmaId);
        return Ok();
    }
}
