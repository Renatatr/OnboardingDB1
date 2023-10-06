using Hospital.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Dados.Mapping;

public class TratamentoMapping : IEntityTypeConfiguration<Tratamento>
{
    public void Configure(EntityTypeBuilder<Tratamento> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Tratamentos");

        builder.Property(x => x.Id).HasColumnName("Id");
        builder.Property(n => n.Nome).HasColumnName("Nome");
        builder.Property(n => n.Periodo).HasColumnName("Periodo");
        builder.Property(n => n.ModoDeUso).HasColumnName("ModoDeUso");
    }
}
