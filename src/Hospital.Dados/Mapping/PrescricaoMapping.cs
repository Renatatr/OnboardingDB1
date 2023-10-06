using Hospital.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Dados.Mapping;

public class PrescricaoMapping : IEntityTypeConfiguration<Prescricao>
{
    public void Configure(EntityTypeBuilder<Prescricao> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Prescricoes");

        builder.Property(x => x.Id).HasColumnName("Id");
        builder.Property(n => n.ConsultaId).HasColumnName("ConsultaId");
        builder.Property(n => n.TratamentoId).HasColumnName("TratamentoId");
        builder.Property(n => n.Diagnostico).HasColumnName("Diagnostico");
        builder.Ignore(x => x.Consultas);
        builder.Ignore(x => x.Tratamentos);
    }
}
