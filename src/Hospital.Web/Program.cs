using Microsoft.EntityFrameworkCore;
using Hospital.Dados.Contexto;
using Hospital.Dominio.Interfaces;
using Hospital.Dominio.Servicos;
using Hospital.Dados.Repositorios;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("HospitalConnection");

builder.Services.AddDbContext<HospitalContext>(options => options.UseLazyLoadingProxies().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add scope.
builder.Services.AddScoped<IMedicoRepository, MedicoRepository>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IConsultaRepository, ConsultaRepository>();
builder.Services.AddScoped<IPrescricaoRepository, PrescricaoRepository>();
builder.Services.AddScoped<ITratamentoRepository, TratamentoRepository>();

builder.Services.AddScoped<IArmazenadorMedico, ArmazenadorDeMedico>();
builder.Services.AddScoped<IArmazenadorPaciente, ArmazenadorDePaciente>();
builder.Services.AddScoped<IArmazenadorConsulta, ArmazenadorDeConsulta>();
builder.Services.AddScoped<IArmazenadorPrescricao, ArmazenadorDePrescricao>();
builder.Services.AddScoped<IArmazenadorTratamento, ArmazenadorDeTratamento>();
builder.Services.AddScoped<IConsultarConsultaMedica, ConsultarConsultaMedica>();

var app = builder.Build();

app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(y => true).AllowCredentials().WithExposedHeaders("Set-Authorization"));
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())+
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
