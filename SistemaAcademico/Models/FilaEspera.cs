namespace SistemaAcademico.Models;

public class FilaEspera
{
    public int Id { get; set; }

    public int AlunoId { get; set; }
    public Usuario? Aluno { get; set; }

    public int TurmaId { get; set; }
    public Turma? Turma { get; set; }

    public DateTime DataEntrada { get; set; } = DateTime.UtcNow;
}
