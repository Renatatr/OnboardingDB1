using Hospital.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Dados.Mapping;

public class MedicoMapping : IEntityTypeConfiguration<Medico>
{
    public void Configure(EntityTypeBuilder<Medico> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Medicos");
        // this.Ignore()
        builder.Property(x => x.Id).HasColumnName("Id");
        builder.Property(n => n.Nome).HasColumnName("Nome");
        builder.Property(n => n.Especialidade).HasColumnName("Especialidade");
        builder.Property(n => n.CPF).HasColumnName("CPF").HasColumnType("varchar(14)");
        builder.Property(n => n.CRM).HasColumnName("CRM").HasColumnType("varchar(8)");
    }
}
