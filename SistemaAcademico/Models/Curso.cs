namespace SistemaAcademico.Models;

public class Curso
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int MaxDisciplinas { get; set; } = 6;

    public ICollection<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();
}
