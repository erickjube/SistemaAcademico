namespace SistemaAcademico.Models;

public class Disciplina
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public int CursoId { get; private set; }
    public Curso? Curso { get; private set; }

    public int? PreRequisitoId { get; private set; }
    public Disciplina? PreRequisito { get; private set; }

    public int CargaHoraria { get; private set; }

    public Disciplina() { }
    public Disciplina(string nome, int cursoId, int cargaHoraria, int? preRequisitoId = null)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome não pode ser vazio.", nameof(nome));
        if (cursoId <= 0) throw new ArgumentException("CursoId deve ser um número positivo.", nameof(cursoId));
        if (cargaHoraria <= 0) throw new ArgumentException("CargaHoraria deve ser um número positivo.", nameof(cargaHoraria));
        if (preRequisitoId.HasValue && preRequisitoId <= 0) throw new ArgumentException("PreRequisitoId deve ser um número positivo.", nameof(preRequisitoId));
        Nome = nome;
        CursoId = cursoId;
        CargaHoraria = cargaHoraria;
        PreRequisitoId = preRequisitoId;
    }

    public void Atualizar(string nome, int cargaHoraria, int? preRequisitoId = null)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome não pode ser vazio.", nameof(nome));
        if (cargaHoraria <= 0) throw new ArgumentException("CargaHoraria deve ser um número positivo.", nameof(cargaHoraria));
        if (preRequisitoId.HasValue && preRequisitoId <= 0) throw new ArgumentException("PreRequisitoId deve ser um número positivo.", nameof(preRequisitoId));
        Nome = nome;
        CargaHoraria = cargaHoraria;
        PreRequisitoId = preRequisitoId;
    }
}
