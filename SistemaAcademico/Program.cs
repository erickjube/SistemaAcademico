using Microsoft.EntityFrameworkCore;
using SistemaAcademico.Data;
using SistemaAcademico.Patterns.Strategy;
using SistemaAcademico.Repositories;
using SistemaAcademico.Repositories.Interfaces;
using SistemaAcademico.Services;
using SistemaAcademico.Services.Interfaces;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração de injeção de dependência do repositório
builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<IDocumentoRepository, DocumentoRepository>();
builder.Services.AddScoped<IDisciplinaRepository, DisciplinaRepository>();
builder.Services.AddScoped<IFilaEsperaRepository, FilaEsperaRepository>();
builder.Services.AddScoped<IMatriculaRepository, MatriculaRepository>();
builder.Services.AddScoped<INotaRepository, NotaRepository>();
builder.Services.AddScoped<IPeriodoLetivoRepository, PeriodoLetivoRepository>();
builder.Services.AddScoped<ISolicitacaoMatriculaEspecialRepository, SolicitacaoMatriculaEspecialRepository>();
builder.Services.AddScoped<ITurmaRepository, TurmaRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Configuração de injeção de dependência da service
builder.Services.AddScoped<IArquivoService, ArquivoService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IDisciplinaService, DisciplinaService>();
builder.Services.AddScoped<IDocumentoService, DocumentoService>();
builder.Services.AddScoped<IMatriculaService, MatriculaService>();
builder.Services.AddScoped<INotaService, NotaService>();
builder.Services.AddScoped<INotificacaoService, NotificacaoService>();
builder.Services.AddScoped<IPdfService, PdfService>();
builder.Services.AddScoped<IPeriodoLetivoService, PeriodoLetivoService>();
builder.Services.AddScoped<ISolicitacaoMatriculaEspecialService, SolicitacaoMatriculaEspecialService>();
builder.Services.AddScoped<ITurmaService, TurmaService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// Configuração de injeção de dependência do factory
builder.Services.AddScoped<CalculadoraFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
