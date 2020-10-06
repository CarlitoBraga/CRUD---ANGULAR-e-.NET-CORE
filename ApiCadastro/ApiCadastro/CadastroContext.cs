using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApiCadastro
{
    public partial class CadastroContext : DbContext
    {
        public CadastroContext()
        {
        }

        public CadastroContext(DbContextOptions<CadastroContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Clientes> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Password=Codigo@123;Persist Security Info=True;User ID=root;Initial Catalog=Cadastro;Data Source=DESKTOP-T15GC90\\SQLEXPRESS");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.HasKey(e => e.Cpf);

                entity.Property(e => e.Cpf).HasColumnName("cpf");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Endereco).HasColumnName("endereco");

                entity.Property(e => e.Nome).HasColumnName("nome");

                entity.Property(e => e.Telefone).HasColumnName("telefone");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
