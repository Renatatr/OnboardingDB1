using Hospital.Dados.Mapping;
using Hospital.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Dados.Contexto;

public class HospitalContext : DbContext
{
    public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
    {

    }

    public DbSet<Medico> Medicos { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Consulta> Consultas { get; set; }
    public DbSet<Prescricao> Prescricoes { get; set; }
    public DbSet<Tratamento> Tratamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new MedicoMapping());
        modelBuilder.ApplyConfiguration(new PacienteMapping());
        modelBuilder.ApplyConfiguration(new ConsultaMapping());
        modelBuilder.ApplyConfiguration(new PrescricaoMapping());
        modelBuilder.ApplyConfiguration(new TratamentoMapping());
    }
}
