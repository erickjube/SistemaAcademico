using SistemaAcademico.DTOs.NotaDto;
using SistemaAcademico.Models;
using SistemaAcademico.Patterns.Strategy;
using SistemaAcademico.Repositories.Interfaces;
using SistemaAcademico.Services.Interfaces;

namespace SistemaAcademico.Services;

public class NotaService : INotaService
{
    private readonly INotaRepository _notaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CalculadoraFactory _factory;

    public NotaService(INotaRepository notaRepository, IUnitOfWork unitOfWork, CalculadoraFactory factory)
    {
        _notaRepository = notaRepository;
        _unitOfWork = unitOfWork;
        _factory = factory;
    }

    public async Task<IEnumerable<NotaResponseDto>> ObterNotasDaTurmaAsync(int turmaId)
    {
        var notas = await _notaRepository.ObterNotasDaTurmaAsync(turmaId);
        if (notas == null || !notas.Any()) return Enumerable.Empty<NotaResponseDto>();

        return notas.Select(n => new NotaResponseDto
        {
            MatriculaId = n.MatriculaId,
            NomeAluno = n.Matricula!.Aluno!.Nome,
            P1 = n.P1,
            P2 = n.P2,
            Trabalho = n.Trabalho,
            Media = n.Media,
            Frequencia = n.Frequencia,
            Situacao = n.Situacao
        });
    }

    public async Task<NotaResponseDto?> ObterNotaAsync(int matriculaId)
    {
        var nota = await _notaRepository.ObterNotaAsync(matriculaId);
        if (nota == null) throw new Exception("Nota não encontrada para a matrícula especificada.");

        return new NotaResponseDto
        {
            MatriculaId = nota.MatriculaId,
            NomeAluno = nota.Matricula!.Aluno!.Nome,
            P1 = nota.P1,
            P2 = nota.P2,
            Trabalho = nota.Trabalho,
            Media = nota.Media,
            Frequencia = nota.Frequencia,
            Situacao = nota.Situacao
        };
    }

    public async Task LancarNotaAsync(CriarNotaDto dto)
    {
        if (dto == null) throw new Exception("Dados de lançamento de nota não podem ser nulos.");

        var calculadora = _factory.Criar(dto.Formula);
        var media = calculadora.Calcular(dto.P1, dto.P2, dto.Trabalho);

        var matricula = await _notaRepository.ObterMatriculaAsync(dto.MatriculaId);
        if (matricula is null) throw new Exception("Matrícula não encontrada.");
        if (matricula.Turma is null) throw new Exception("Turma associada à matrícula não encontrada.");

        if (matricula.Turma.Fechada == true) throw new Exception("Não é possível lançar notas em uma turma fechada.");

        var notaExistente = await _notaRepository.ObterNotaAsync(dto.MatriculaId);
        if (notaExistente is not null) throw new Exception("Já existe uma nota lançada para esta matrícula.");

        if (!matricula.Aluno!.Ativo) throw new InvalidOperationException("Não é possível lançar notas para um aluno desativado.");

        var nota = new Nota
        (
            dto.MatriculaId,
            dto.P1,
            dto.P2,
            dto.Trabalho,
            media,
            dto.Frequencia
        );

        await _notaRepository.AdicionarNotaAsync(nota);
        await _unitOfWork.SalvarAsync();
    }

    public async Task AtualizarNotaAsync(CriarNotaDto dto)
    {
        var calculadora = _factory.Criar(dto.Formula);
        var media = calculadora.Calcular(dto.P1, dto.P2, dto.Trabalho);

        var nota = await _notaRepository.ObterNotaAsync(dto.MatriculaId);
        if (nota is null) throw new Exception("Nota não encontrada para a matrícula especificada.");

        var matricula = nota.Matricula;
        if (matricula is null) throw new Exception("Matrícula associada à nota não encontrada.");
        if (matricula.Turma is null) throw new Exception("Turma associada à matrícula não encontrada.");
        if (matricula.Turma.Fechada == true) throw new Exception("Não é possível alterar notas de uma turma fechada.");
        if (!matricula.Aluno!.Ativo) throw new InvalidOperationException("Não é possível atualizar notas para um aluno desativado.");

        nota.AtualizarNota(dto.P1, dto.P2, dto.Trabalho, dto.Frequencia, media);
        await _unitOfWork.SalvarAsync();
    }

    public async Task FecharDiarioAsync(int turmaId)
    {
        var turma = await _notaRepository.ObterTurmaParaFechamentoAsync(turmaId);
        if (turma is null) throw new Exception("Turma não encontrada.");

        if (turma.Fechada) throw new Exception("Diário já fechado.");

        if (!turma.Matriculas.Any()) throw new Exception("A turma não possui alunos matriculados.");

        foreach (var matricula in turma.Matriculas)
        {
            if (matricula.Nota is null && matricula.Aluno!.Ativo) 
                throw new Exception($"Aluno {matricula.Aluno?.Nome} está sem nota.");
            if (matricula.Nota?.Frequencia == 0 && matricula.Aluno!.Ativo) 
                throw new Exception($"Aluno {matricula.Aluno.Nome} está sem frequência.");
        }

        turma.FecharDiario();
        await _unitOfWork.SalvarAsync();
    }    
}
