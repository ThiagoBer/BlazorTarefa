using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlazorTarefa.Models;

namespace BlazorTarefa.Dados
{
    public class TarefaConfig : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            // configurar campos para ficar todos em minusculos no postgres, chaves e indices
            builder.ToTable("tarefas"); 

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                   .HasDefaultValueSql("gen_random_uuid()")
                   .ValueGeneratedOnAdd();

            builder.Property(t => t.Descricao).HasColumnName("descricao").IsRequired();
            builder.Property(t => t.Concluido).HasColumnName("concluido").HasDefaultValue(false);
            builder.Property(t => t.IdPai).HasColumnName("id_pai");
            builder.Property(t => t.CriadoEm).HasColumnName("criado_em").HasDefaultValueSql("now()");
            builder.Property(t => t.ConcluidoEm).HasColumnName("concluido_em");

            builder.HasMany(t => t.SubTarefas)
                   .WithOne()
                   .HasForeignKey(t => t.IdPai)
                   .OnDelete(DeleteBehavior.Cascade); 

            builder.HasIndex(t => t.IdPai);
            builder.HasIndex(t => t.Concluido);
        }
    }
}