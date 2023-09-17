using ListaDeTarefas.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ListaDeTarefas.Infra.Data.Mappings
{
    public sealed class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.Ignore(x => x.Id);
            builder.HasKey(x => x.UsuarioId);

            builder.OwnsOne(x => x.Login)
                .Property(x => x.Username)
                .HasColumnName("username")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired(true);

            builder.OwnsOne(x => x.Senha)
               .Property(x => x.Password)
               .HasColumnName("Senha")
               .HasColumnType("varchar")
               .HasMaxLength(64)
               .IsRequired(true);

            builder.OwnsOne(x => x.Senha)
               .Property(x => x.CodigoAlteracao)
               .HasColumnName("CodigoAlteracaoSenha")
               .HasColumnType("varchar")
               .HasMaxLength(8)
               .IsRequired(true);

            builder.OwnsOne(x => x.Email)
               .Property(x => x.Endereco)
               .HasColumnName("Email")
               .HasColumnType("varchar")
               .HasMaxLength(60)
               .IsRequired(true);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(x => x.VerificarEmail)
                .Property(x => x.Codigo)
                .HasColumnName("CodigoVerificacaoEmail")
                .HasColumnType("varchar")
                .HasMaxLength(6)
                .IsRequired(true);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(x => x.VerificarEmail)
                .Property(x => x.DataExpiracao)
                .HasColumnName("DataExpiracaoCodigoEmail")
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(x => x.VerificarEmail)
                .Property(x => x.DataVerificacao)
                .HasColumnName("DataVerificacaoCodigoEmail")
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(x => x.VerificarEmail)
                .Ignore(x => x.Ativo);

            builder.HasMany(x => x.Tarefas)
                .WithOne(x => x.Usuario)
                .HasForeignKey(x => x.FkUsuario);
        }
    }
}
