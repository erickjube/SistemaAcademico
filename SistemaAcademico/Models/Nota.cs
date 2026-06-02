using SistemaAcademico.ENUMs;
using System.ComponentModel.DataAnnotations;

namespace SistemaAcademico.Models;

public class Nota
{
    public int Id { get; private set; }

    public int MatriculaId { get; private set; }
    public Matricula? Matricula { get; private set; }

    public decimal P1 { get; private set; }
    public decimal P2 { get; private set; }
    public decimal Trabalho { get; private set; }

    public decimal Media { get; private set; }
    public SituacaoAluno Situacao { get; private set; }

    public decimal Frequencia { get; private set; }

    [Timestamp]
    public byte[]? RowVersion { get; private set; }

    public Nota() { }

    public Nota(int matriculaId, decimal p1, decimal p2, decimal trabalho, decimal frequencia, decimal media)
    {
        if (matriculaId <= 0) throw new ArgumentException("MatriculaId deve ser um número positivo.", nameof(matriculaId));
        if (p1 < 0 || p1 > 10) throw new ArgumentException("P1 deve estar entre 0 e 10.", nameof(p1));
        if (p2 < 0 || p2 > 10) throw new ArgumentException("P2 deve estar entre 0 e 10.", nameof(p2));
        if (trabalho < 0 || trabalho > 10) throw new ArgumentException("Trabalho deve estar entre 0 e 10.", nameof(trabalho));
        if (frequencia < 0 || frequencia > 100) throw new ArgumentException("Frequencia deve estar entre 0 e 100.", nameof(frequencia));
        if (media < 0 || media > 10) throw new ArgumentException("Media deve estar entre 0 e 10.", nameof(media));

        MatriculaId = matriculaId;
        P1 = p1;
        P2 = p2;
        Trabalho = trabalho;
        Frequencia = frequencia;
        Media = media;
        AtualizarSituacao();
    }

    public void AtualizarNota(decimal p1, decimal p2, decimal trabalho, decimal frequencia, decimal media)
    {
        if (p1 < 0 || p1 > 10) throw new ArgumentException("P1 deve estar entre 0 e 10.", nameof(p1));
        if (p2 < 0 || p2 > 10) throw new ArgumentException("P2 deve estar entre 0 e 10.", nameof(p2));
        if (trabalho < 0 || trabalho > 10) throw new ArgumentException("Trabalho deve estar entre 0 e 10.", nameof(trabalho));
        if (frequencia < 0 || frequencia > 100) throw new ArgumentException("Frequencia deve estar entre 0 e 100.", nameof(frequencia));
        if (media < 0 || media > 10) throw new ArgumentException("Media deve estar entre 0 e 10.", nameof(media));
        P1 = p1;
        P2 = p2;
        Trabalho = trabalho;
        Frequencia = frequencia;
        Media = media;
        AtualizarSituacao();
    }

    public void AtualizarSituacao()
    {
        if (Media >= 6.0m && Frequencia >= 75.0m)
            Situacao = SituacaoAluno.Aprovado;
        else if (Media < 6.0m && Frequencia >= 75.0m)
            Situacao = SituacaoAluno.ReprovadoPorNota;
        else
            Situacao = SituacaoAluno.ReprovadoPorFalta;
    }
}
