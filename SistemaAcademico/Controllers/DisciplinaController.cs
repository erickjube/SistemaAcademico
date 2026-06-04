using Microsoft.AspNetCore.Mvc;
using SistemaAcademico.DTOs.CursoDto;
using SistemaAcademico.DTOs.DisciplinaDto;
using SistemaAcademico.Services.Interfaces;

namespace SistemaAcademico.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DisciplinaController : ControllerBase
{
    private readonly IDisciplinaService _disciplinaService;

    public DisciplinaController(IDisciplinaService disciplinaService)
    {
        _disciplinaService = disciplinaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DisciplinaDto>>> GetAll()
    {
        var disciplinas = await _disciplinaService.ObterTodasDisciplinasAsync();
        return Ok(disciplinas);
    }

    [HttpGet("{DisciplinaId}", Name = "ObterDisciplina")]
    public async Task<ActionResult<DisciplinaDto>> GetById(int DisciplinaId)
    {
        var disciplina = await _disciplinaService.ObterDisciplinaPorIdAsync(DisciplinaId);
        return Ok(disciplina);
    }

    [HttpPost]
    public async Task<ActionResult<DisciplinaDto>> Create(CriarDisciplinaDto dto)
    {
        var disciplina = await _disciplinaService.CriarDisciplinaAsync(dto);
        return CreatedAtRoute("ObterDisciplina", new { DisciplinaId = disciplina.Id }, disciplina);
    }

    [HttpPut]
    public async Task<ActionResult> Update(DisciplinaDto dto)
    {
        await _disciplinaService.AtualizarDisciplinaAsync(dto);
        return NoContent();
    }

    [HttpDelete("{DisciplinaId}")]
    public async Task<ActionResult> Delete(int DisciplinaId)
    {
        await _disciplinaService.ExcluirDisciplinaAsync(DisciplinaId);
        return NoContent();
    }
}
