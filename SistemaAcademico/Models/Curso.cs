namespace SistemaAcademico.Models;

public class Curso
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public int MaxDisciplinas { get; private set; } = 6;

    public ICollection<Disciplina> Disciplinas { get; private set; } = new List<Disciplina>();

    public Curso() { }
    public Curso(string nome, int maxDisciplinas)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome do curso é obrigatório.", nameof(nome));
        if (maxDisciplinas <= 0) throw new ArgumentException("MaxDisciplinas deve ser um número positivo.", nameof(maxDisciplinas));
        Nome = nome;
        MaxDisciplinas = maxDisciplinas;
    }

    public void Atualizar(string nome, int maxDisciplinas)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome do curso é obrigatório.", nameof(nome));
        if (maxDisciplinas <= 0) throw new ArgumentException("MaxDisciplinas deve ser um número positivo.", nameof(maxDisciplinas));
        Nome = nome;
        MaxDisciplinas = maxDisciplinas;
    }
}
