using SistemaAcademico.ENUMs;
using System.ComponentModel.DataAnnotations;

namespace SistemaAcademico.Models;

public class Nota
{
    public int Id { get; set; }

    public int MatriculaId { get; set; }
    public Matricula? Matricula { get; set; }

    public decimal P1 { get; set; }
    public decimal P2 { get; set; }
    public decimal Trabalho { get; set; }

    public decimal Media { get; set; }
    public SituacaoAluno Situacao { get; set; }

    public decimal Frequencia { get; set; }

    [Timestamp]
    public byte[]? RowVersion { get; set; }
}
