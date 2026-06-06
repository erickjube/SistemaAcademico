using Microsoft.AspNetCore.Mvc;
using SistemaAcademico.DTOs.MatriculaDto;
using SistemaAcademico.DTOs.MatriculaEspecialDto;
using SistemaAcademico.Models;
using SistemaAcademico.Services.Interfaces;

namespace SistemaAcademico.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatriculaController : ControllerBase
{
    private readonly IMatriculaService _matriculaService;
    private readonly ISolicitacaoMatriculaEspecialService _especialService;

    public MatriculaController(
        IMatriculaService matriculaService,
        ISolicitacaoMatriculaEspecialService especialService)
    {
        _matriculaService = matriculaService;
        _especialService = especialService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MatriculaResponseDto>>> ObterMatriculas()
    {
        var matriculas = await _matriculaService.ObterMatriculasAsync();
        return Ok(matriculas);
    }

    [HttpGet("{matriculaId}", Name = "ObterMatricula")] 
    public async Task<ActionResult<MatriculaResponseDto>> ObterMatricula(int matriculaId)
    {
        var matricula = await _matriculaService.ObterMatriculaPorIdAsync(matriculaId);
        return Ok(matricula);
    }

    [HttpGet("aluno/{alunoId}")] 
    public async Task<ActionResult<IEnumerable<MatriculaResponseDto>>> ObterMatriculasAluno(int alunoId)
    {
        var matriculas = await _matriculaService.ObterMatriculaAsync(alunoId);
        return Ok(matriculas);
    }

    [HttpGet("disponiveis")]
    public async Task<ActionResult> ObterTurmasDisponiveis(int alunoId)
    {
        var turmas = await _matriculaService.ObterTurmasDisponiveisAsync(alunoId);
        return Ok(turmas);
    }

    [HttpPost]
    public async Task<ActionResult<MatriculaResponseDto>> CriarMatricula(MatriculaCriarDto dto)
    {
        var matricula = await _matriculaService.CriarMatriculaAsync(dto);
        return CreatedAtRoute("ObterMatricula", new { MatriculaId = matricula.Id }, matricula);
    }

    [HttpDelete("{matriculaId}")]
    public async Task<ActionResult> CancelarMatricula(int matriculaId)
    {
        await _matriculaService.CancelarMatriculaAsync(matriculaId);
        return NoContent();
    }

    [HttpPost("especial")]
    public async Task<ActionResult> SolicitarMatriculaEspecial(SolicitacaoMatriculaEspecialCriarDto dto)
    {
        await _especialService.SolicitarMatriculaEspecialAsync(dto);
        return Ok(new
        {
            Mensagem = "Solicitação enviada para análise."
        });
    }

    [HttpGet("especial")]
    public async Task<ActionResult<IEnumerable<MatriculaResponseDto>>> ObterSolicitacoes()
    {
        var solicitacoes = await _especialService.ObterSolicitacoesMatriculaEspecialAsync();
        return Ok(solicitacoes);
    }

    [HttpPut("especial/{id}/aprovar")]
    public async Task<ActionResult> AprovarSolicitacao(int solicitacaoId)
    {
        await _especialService.AprovarMatriculaEspecialAsync(solicitacaoId);
        return Ok(new
        {
            Mensagem = "Solicitação aprovada."
        });
    }

    [HttpPut("especial/{id}/rejeitar")]
    public async Task<ActionResult> RejeitarSolicitacao(int solicitacaoId)
    {
        await _especialService.RejeitarMatriculaEspecialAsync(solicitacaoId);
        return Ok(new
        {
            Mensagem = "Solicitação rejeitada."
        });
    }


}