﻿// <auto-generated />
using ListaDeTarefas.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ListaDeTarefas.Infra.Migrations
{
    [DbContext(typeof(TarefasDbContext))]
    [Migration("20230907014035_Criacao")]
    partial class Criacao
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ListaDeTarefas.Domain.Models.Tarefa", b =>
                {
                    b.Property<int>("TarefaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TarefaId"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar")
                        .HasColumnName("Descricao");

                    b.Property<bool>("Finalizada")
                        .HasColumnType("bit")
                        .HasColumnName("Finalizada");

                    b.Property<int>("FkUsuario")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar")
                        .HasColumnName("Titulo");

                    b.HasKey("TarefaId");

                    b.HasIndex("FkUsuario");

                    b.ToTable("Tarefa", (string)null);
                });

            modelBuilder.Entity("ListaDeTarefas.Domain.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioId"));

                    b.Property<int>("FkTarefa")
                        .HasColumnType("int");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("ListaDeTarefas.Domain.Models.Tarefa", b =>
                {
                    b.HasOne("ListaDeTarefas.Domain.Models.Usuario", "Usuario")
                        .WithMany("Tarefas")
                        .HasForeignKey("FkUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ListaDeTarefas.Domain.Models.Usuario", b =>
                {
                    b.OwnsOne("ListaDeTarefas.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<int>("UsuarioId")
                                .HasColumnType("int");

                            b1.Property<string>("Endereco")
                                .IsRequired()
                                .HasMaxLength(60)
                                .HasColumnType("varchar")
                                .HasColumnName("Email");

                            b1.HasKey("UsuarioId");

                            b1.ToTable("Usuario");

                            b1.WithOwner()
                                .HasForeignKey("UsuarioId");
                        });

                    b.OwnsOne("ListaDeTarefas.Domain.ValueObjects.Login", "Login", b1 =>
                        {
                            b1.Property<int>("UsuarioId")
                                .HasColumnType("int");

                            b1.Property<string>("Username")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar")
                                .HasColumnName("username");

                            b1.HasKey("UsuarioId");

                            b1.ToTable("Usuario");

                            b1.WithOwner()
                                .HasForeignKey("UsuarioId");
                        });

                    b.OwnsOne("ListaDeTarefas.Domain.ValueObjects.Senha", "Senha", b1 =>
                        {
                            b1.Property<int>("UsuarioId")
                                .HasColumnType("int");

                            b1.Property<string>("Password")
                                .IsRequired()
                                .HasMaxLength(64)
                                .HasColumnType("varchar")
                                .HasColumnName("Senha");

                            b1.HasKey("UsuarioId");

                            b1.ToTable("Usuario");

                            b1.WithOwner()
                                .HasForeignKey("UsuarioId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Login")
                        .IsRequired();

                    b.Navigation("Senha")
                        .IsRequired();
                });

            modelBuilder.Entity("ListaDeTarefas.Domain.Models.Usuario", b =>
                {
                    b.Navigation("Tarefas");
                });
#pragma warning restore 612, 618
        }
    }
}