using Microsoft.AspNetCore.Mvc;
using SistemaAcademico.DTOs.TurmaDto;
using SistemaAcademico.Models;
using SistemaAcademico.Services.Interfaces;

namespace SistemaAcademico.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TurmaController : ControllerBase
{
    private readonly ITurmaService _turmaService;

    public TurmaController(ITurmaService turmaService)
    {
        _turmaService = turmaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TurmaResponseDto>>> GetAll()
    {
        var turmas = await _turmaService.ObterTodosAsync();
        return Ok(turmas);
    }

    [HttpGet("{TurmaId}", Name = "ObterTurma")]
    public async Task<ActionResult<TurmaResponseDto>> GetById(int turmaId)
    {
        var turma = await _turmaService.ObterPorIdAsync(turmaId);
        return Ok(turma);
    }

    [HttpPost]
    public async Task<ActionResult<TurmaResponseDto>> Create(CriarTurmaDto dto)
    {
        var turma = await _turmaService.CriarAsync(dto);
        return CreatedAtRoute("ObterTurma", new { TurmaId = turma.Id }, turma);
    }

    [HttpPut]
    public async Task<ActionResult> Update(UpdateTurmaDto dto)
    {
        await _turmaService.Update(dto);
        return NoContent();
    }
}
