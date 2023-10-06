using Hospital.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Dados.Mapping;

public class ConsultaMapping : IEntityTypeConfiguration<Consulta>
{
    public void Configure(EntityTypeBuilder<Consulta> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Consultas");

        builder.Property(x => x.Id).HasColumnName("Id");
        builder.Property(n => n.MedicoId).HasColumnName("MedicoId");
        builder.Property(n => n.PacienteId).HasColumnName("PacienteId");
        builder.Property(n => n.Data).HasColumnName("Data");
        builder.Property(n => n.DuracaoMin).HasColumnName("DuracaoMin").HasColumnType("int");
        builder.Ignore(x => x.Medico);
        builder.Ignore(x => x.Paciente);
    }
}
