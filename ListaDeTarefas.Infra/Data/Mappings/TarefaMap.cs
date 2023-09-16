using ListaDeTarefas.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ListaDeTarefas.Infra.Data.Mappings
{
    public sealed class TarefaMap : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.ToTable("Tarefa");

            builder.Ignore(x => x.Id);

            builder.HasKey(x => x.TarefaId);

            builder.Property(x => x.Titulo)
                .HasColumnName("Titulo")
                .HasColumnType("varchar")
                .HasMaxLength(30)
                .IsRequired(true);

            builder.Property(x => x.Descricao)
               .HasColumnName("Descricao")
               .HasColumnType("varchar")
               .HasMaxLength(300)
               .IsRequired(true);

            builder.Property(x => x.Finalizada)
               .HasColumnName("Finalizada")
               .HasColumnType("bit")
               .IsRequired(true);

            builder.Property(x => x.DataEntrega)
               .HasColumnName("DataEntrega")
               .HasColumnType("datetime")
               .IsRequired(true);

            builder.HasOne(x => x.Usuario)
                .WithMany(u => u.Tarefas)
                .HasForeignKey(x => x.FkUsuario);

        }
    }
}
