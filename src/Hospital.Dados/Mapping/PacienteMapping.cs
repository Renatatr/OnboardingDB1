using Hospital.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Dados.Mapping;

public class PacienteMapping : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Pacientes");
        // this.Ignore()
        builder.Property(x => x.Id).HasColumnName("Id");
        builder.Property(n => n.Nome).HasColumnName("Nome");
        builder.Property(n => n.Nascimento).HasColumnName("DataNascimento");
        builder.Property(n => n.Acompanhante).HasColumnName("Acompanhante").HasColumnType("varchar(80)").IsRequired(false);
        builder.Property(n => n.CPF).HasColumnName("CPF").HasColumnType("varchar(14)");
    }
}
