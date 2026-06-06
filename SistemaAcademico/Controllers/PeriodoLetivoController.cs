using Microsoft.AspNetCore.Mvc;
using SistemaAcademico.DTOs.CursoDto;
using SistemaAcademico.DTOs.PeriodoLetivoDto;
using SistemaAcademico.Services;
using SistemaAcademico.Services.Interfaces;

namespace SistemaAcademico.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeriodoLetivoController : ControllerBase
{
    private readonly IPeriodoLetivoService _periodoLetivoService;

    public PeriodoLetivoController(IPeriodoLetivoService periodoLetivoService)
    {
        _periodoLetivoService = periodoLetivoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PeriodoDto>>> GetAll()
    {
        var periodos = await _periodoLetivoService.ObterTodosPeriodosAsync();
        return Ok(periodos);
    }

    [HttpGet("{PeriodoId}", Name = "ObterPeriodo")]
    public async Task<ActionResult<PeriodoDto>> GetById(int periodoId)
    {
        var periodo = await _periodoLetivoService.ObterPeriodoPorIdAsync(periodoId);
        return Ok(periodo);
    }

    [HttpPost]
    public async Task<ActionResult<PeriodoDto>> Create(CriarPeriodoDto dto)
    {
        var periodo = await _periodoLetivoService.CriarPeriodoAsync(dto);
        return CreatedAtRoute("ObterPeriodo", new { PeriodoLetivoId = periodo.Id }, periodo);
    }

    [HttpDelete("{PeriodoId}")]
    public async Task<ActionResult> Delete(int periodoId)
    {
        await _periodoLetivoService.ExcluirPeriodoAsync(periodoId);
        return NoContent();
    }
}
