namespace SistemaAcademico.Models;

public class Disciplina
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int CursoId { get; set; }
    public Curso? Curso { get; set; }

    public int? PreRequisitoId { get; set; }
    public Disciplina? PreRequisito { get; set; }

    public int CargaHoraria { get; set; }
}
