using Microsoft.EntityFrameworkCore;
using SistemaAcademico.Models;

namespace SistemaAcademico.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Curso> Cursos => Set<Curso>();
    public DbSet<Disciplina> Disciplinas => Set<Disciplina>();
    public DbSet<PeriodoLetivo> PeriodosLetivos => Set<PeriodoLetivo>();
    public DbSet<Turma> Turmas => Set<Turma>();
    public DbSet<Matricula> Matriculas => Set<Matricula>();
    public DbSet<Nota> Notas => Set<Nota>();
    public DbSet<DocumentoGerado> DocumentosGerados => Set<DocumentoGerado>();
    public DbSet<Notificacao> Notificacoes => Set<Notificacao>();
    public DbSet<FilaEspera> FilaEsperas => Set<FilaEspera>();
    public DbSet<SolicitacaoMatriculaEspecial> SolicitacoesMatriculaEspecial => Set<SolicitacaoMatriculaEspecial>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // usuário
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Nome)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(x => x.Email)
                .HasMaxLength(150)
                .IsRequired();

            entity.HasIndex(x => x.Email)
                .IsUnique();

            entity.Property(x => x.SenhaHash)
                .IsRequired();

            entity.Property(x => x.Perfil)
                .HasConversion<string>();
        });

        // curso
        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Nome)
                .HasMaxLength(150)
                .IsRequired();
        });

        // disciplina
        modelBuilder.Entity<Disciplina>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Nome)
                .HasMaxLength(150)
                .IsRequired();

            entity.HasOne(d => d.PreRequisito)
                .WithMany()
                .HasForeignKey(d => d.PreRequisitoId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // turma
        modelBuilder.Entity<Turma>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.HasOne(t => t.Professor)
                .WithMany(u => u.TurmasProfessor)
                .HasForeignKey(t => t.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Disciplina)
                .WithMany(d => d.Turmas)
                .HasForeignKey(t => t.DisciplinaId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.PeriodoLetivo)
                .WithMany()
                .HasForeignKey(t => t.PeriodoLetivoId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // matricula
        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.HasIndex(x => new { x.AlunoId, x.TurmaId })
                .IsUnique();

            entity.Property(x => x.Status)
                .HasMaxLength(50)
                .IsRequired();

            entity.HasOne(m => m.Aluno)
                .WithMany(u => u.MatriculasAluno)
                .HasForeignKey(m => m.AlunoId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(m => m.Turma)
                .WithMany(t => t.Matriculas)
                .HasForeignKey(m => m.TurmaId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // nota
        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.P1)
                .HasPrecision(5, 2);

            entity.Property(x => x.P2)
                .HasPrecision(5, 2);

            entity.Property(x => x.Trabalho)
                .HasPrecision(5, 2);

            entity.Property(x => x.Media)
                .HasPrecision(5, 2);

            entity.Property(x => x.Frequencia)
                .HasPrecision(5, 2);

            entity.Property(x => x.Situacao)
                .HasMaxLength(50);

            entity.Property(x => x.RowVersion)
                .IsRowVersion();

            entity.HasOne(n => n.Matricula)
                .WithOne(m => m.Nota)
                .HasForeignKey<Nota>(n => n.MatriculaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // documento gerado
        modelBuilder.Entity<DocumentoGerado>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.HashSha256)
                .HasMaxLength(128)
                .IsRequired();

            entity.Property(x => x.CaminhoPdf)
                .HasMaxLength(300)
                .IsRequired();

            entity.Property(x => x.Tipo)
                .HasConversion<string>();

            entity.HasOne(d => d.Usuario)
                .WithMany()
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // notificação
        modelBuilder.Entity<Notificacao>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Titulo)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(x => x.Mensagem)
                .HasMaxLength(1000)
                .IsRequired();

            entity.HasOne(n => n.Usuario)
                .WithMany()
                .HasForeignKey(n => n.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Fila espera
        modelBuilder.Entity<FilaEspera>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.HasOne(f => f.Aluno)
                .WithMany()
                .HasForeignKey(f => f.AlunoId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(f => f.Turma)
                .WithMany()
                .HasForeignKey(f => f.TurmaId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Solicitação matrícula especial
        modelBuilder.Entity<SolicitacaoMatriculaEspecial>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Justificativa)
                .HasMaxLength(1000)
                .IsRequired();
            entity.Property(x => x.Status)
                .HasConversion<string>();
            entity.HasOne(s => s.Aluno)
                .WithMany()
                .HasForeignKey(s => s.AlunoId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(s => s.Turma)
                .WithMany()
                .HasForeignKey(s => s.TurmaId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
