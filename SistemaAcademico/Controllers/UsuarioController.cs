using Microsoft.AspNetCore.Mvc;
using SistemaAcademico.DTOs.UsuarioDto;
using SistemaAcademico.Services.Interfaces;

namespace SistemaAcademico.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet("{email}")]
    public async Task<ActionResult<UsuarioResponseDto>> GetByEmail(string email)
    {
        var usuario = await _usuarioService.ObterPorEmailAsync(email);
        return Ok(usuario);
    }

    [HttpGet("{usuarioId}", Name = "ObterUsuario")]
    public async Task<ActionResult<UsuarioResponseDto>> GetById(int usuarioId)
    {
        var usuario = await _usuarioService.ObterPorIdAsync(usuarioId);
        return Ok(usuario);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> GetAll()
    {
        var usuarios = await _usuarioService.ObterTodos();
        return Ok(usuarios);
    }

    [HttpGet("professores")]
    public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> GetProfessores()
    {
        var professores = await _usuarioService.ObterProfessoresAsync();
        return Ok(professores);
    }

    [HttpGet("alunos")]
    public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> GetAlunos()
    {
        var alunos = await _usuarioService.ObterAlunosAsync();
        return Ok(alunos);
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioResponseDto>> Create(CriarUsuarioDto dto)
    {
        var usuario = await _usuarioService.AdicionarAsync(dto);
        return CreatedAtRoute("ObterUsuario", new { usuarioId = usuario.Id }, usuario);
    }

    [HttpPut]
    public async Task<ActionResult> Update(AtualizarUsuarioDto dto)
    {
        await _usuarioService.AtualizarAsync(dto);
        return NoContent();
    }

    [HttpPatch("{usuarioId}/desativar")]
    public async Task<ActionResult> Desativar(int usuarioId)
    {
        await _usuarioService.DesativarAsync(usuarioId);
        return NoContent();
    }
}
