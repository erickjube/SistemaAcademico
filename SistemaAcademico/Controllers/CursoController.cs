using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAcademico.Data;
using SistemaAcademico.DTOs.CursoDto;
using SistemaAcademico.Models;
using SistemaAcademico.Repositories.Interfaces;
using SistemaAcademico.Services.Interfaces;

namespace SistemaAcademico.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CursoController : ControllerBase
{
    private readonly ICursoService _cursoService;

    public CursoController(ICursoService cursoService)
    {
        _cursoService = cursoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CursoDto>>> GetAll()
    {
        var cursos = await _cursoService.ObterTodosCursosAsync();
        return Ok(cursos);
    }

    [HttpGet("{CursoId}", Name = "ObterCurso")]
    public async Task<ActionResult<CursoDto>> GetById(int CursoId)
    {
        var curso = await _cursoService.ObterCursoPorIdAsync(CursoId);
        return Ok(curso);
    }

    [HttpPost]
    public async Task<ActionResult<CursoDto>> Create(CriarCursoDto dto)
    {
        var curso = await _cursoService.CriarCursoAsync(dto);
        return CreatedAtRoute("ObterCurso", new { CursoId = curso.Id }, curso);
    }

    [HttpPut]
    public async Task<ActionResult<CursoDto>> Update(CursoDto dto)
    {
        var curso = await _cursoService.AtualizarCursoAsync(dto);
        return Ok(curso);
    }

    [HttpDelete("{CursoId}")]
    public async Task<ActionResult> Delete(int CursoId)
    {
        await _cursoService.ExcluirCursoAsync(CursoId);
        return NoContent();
    }
}
