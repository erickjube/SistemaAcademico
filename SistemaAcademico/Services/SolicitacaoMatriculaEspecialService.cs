using SistemaAcademico.DTOs.MatriculaEspecialDto;
using SistemaAcademico.Models;
using SistemaAcademico.Repositories.Interfaces;
using SistemaAcademico.Services.Interfaces;

namespace SistemaAcademico.Services;

public class SolicitacaoMatriculaEspecialService : ISolicitacaoMatriculaEspecialService
{
    private readonly ISolicitacaoMatriculaEspecialRepository _solicitacaoMatriculaEspecialRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMatriculaRepository _matriculaRepository;

    public SolicitacaoMatriculaEspecialService(ISolicitacaoMatriculaEspecialRepository solicitacaoMatriculaEspecialRepository, 
                                               IUnitOfWork unitOfWork,
                                               IMatriculaRepository matriculaRepository)
    {
        _solicitacaoMatriculaEspecialRepository = solicitacaoMatriculaEspecialRepository;
        _unitOfWork = unitOfWork;
        _matriculaRepository = matriculaRepository;
    }

    public async Task<IEnumerable<SolicitacaoMatriculaEspecialResponseDto>> ObterSolicitacoesMatriculaEspecialAsync()
    {
        var matriculas = await _solicitacaoMatriculaEspecialRepository.ObterSolicitacoesAsync();
        if (matriculas == null) throw new Exception("Matrículas não encontradas para a turma especificada.");
        return matriculas.Select(m => new SolicitacaoMatriculaEspecialResponseDto
        {
            Id = m.Id,
            AlunoId = m.AlunoId,
            TurmaId = m.TurmaId,
            AlunoNome = m.Aluno?.Nome,
            TurmaNome = m.Turma?.Disciplina?.Nome,
            Status = m.Status,
            DataSolicitacao = m.DataSolicitacao,
            Justificativa = m.Justificativa,
        });
    }

    public async Task SolicitarMatriculaEspecialAsync(SolicitacaoMatriculaEspecialCriarDto dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto), "O DTO de criação de matrícula especial não pode ser nulo.");

        var solicitacao = new SolicitacaoMatriculaEspecial(
            dto.AlunoId,
            dto.TurmaId,
            dto.Justificativa
        );

        await _solicitacaoMatriculaEspecialRepository.AdicionarSolicitacaoMatriculaEspecialAsync(solicitacao);
        await _unitOfWork.SalvarAsync();
    }

    public async Task AprovarMatriculaEspecialAsync(int solicitacaoId)
    {
        var solicitacao = await _solicitacaoMatriculaEspecialRepository.ObterSolicitacaoAsync(solicitacaoId);
        if (solicitacao == null) throw new Exception("Solicitação de matrícula especial não encontrada para o ID especificado.");

        solicitacao.Aprovar();

        var matricula = new Matricula(
        solicitacao.AlunoId,
        solicitacao.TurmaId);

        matricula.TornarEspecial(solicitacao.Justificativa);

        await _matriculaRepository.AdicionarMatriculaAsync(matricula);
        await _unitOfWork.SalvarAsync();
    }

    public async Task RejeitarMatriculaEspecialAsync(int solicitacaoId)
    {
        var solicitacao = await _solicitacaoMatriculaEspecialRepository.ObterSolicitacaoAsync(solicitacaoId);
        if (solicitacao == null) throw new Exception("Solicitação de matrícula especial não encontrada para o ID especificado.");

        solicitacao.Rejeitar();
        await _unitOfWork.SalvarAsync();
    }
}
