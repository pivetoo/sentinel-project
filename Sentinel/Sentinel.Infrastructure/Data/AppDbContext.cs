using Microsoft.EntityFrameworkCore;
using Sentinel.Domain.Entities;

namespace Sentinel.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UsuarioSentinel> UsuariosSentinel { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Plano> Planos { get; set; }
        public DbSet<UsuarioEmpresa> UsuariosEmpresa { get; set; }
        public DbSet<Papel> Papeis { get; set; }
        public DbSet<Permissao> Permissoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.UseSnakeCase();

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.ToTable("empresa");
                entity.HasIndex(e => e.TenantId).IsUnique();
                entity.HasIndex(e => e.Cnpj).IsUnique();

                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.TenantId).IsRequired();
                entity.Property(e => e.Cnpj).IsRequired();
                entity.Property(e => e.Email).IsRequired();
            });

            modelBuilder.Entity<Contrato>(entity =>
            {
                entity.ToTable("contrato");

                entity.HasOne(c => c.Empresa)
                    .WithMany(e => e.Contratos)
                    .HasForeignKey(c => c.EmpresaId);

                entity.HasOne(c => c.Plano)
                .WithMany(p => p.Contratos)
                    .HasForeignKey(c => c.PlanoId);
            });

            modelBuilder.Entity<Plano>(entity =>
            {
                entity.ToTable("plano");
                entity.Property(p => p.Nome).IsRequired();
                entity.Property(p => p.ValorMensal).IsRequired();
            });

            modelBuilder.Entity<UsuarioSentinel>(entity =>
            {
                entity.ToTable("usuario_sentinel");
                entity.HasIndex(u => u.Login).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();

                entity.Property(u => u.Login).IsRequired();
                entity.Property(u => u.Senha).IsRequired();
                entity.Property(u => u.Email).IsRequired();
            });

            modelBuilder.Entity<UsuarioEmpresa>(entity =>
            {
                entity.ToTable("usuario_empresa");
                entity.HasIndex(u => new { u.EmpresaId, u.Login }).IsUnique();
                entity.HasIndex(u => new { u.EmpresaId, u.Email }).IsUnique();

                entity.Property(u => u.Login).IsRequired();
                entity.Property(u => u.Senha).IsRequired();
                entity.Property(u => u.Email).IsRequired();

                entity.HasOne(u => u.Empresa)
                    .WithMany(e => e.Usuarios)
                    .HasForeignKey(u => u.EmpresaId);
            });

            modelBuilder.Entity<Papel>(entity =>
            {
                entity.ToTable("papel");
                entity.Property(p => p.Nome).IsRequired();

                entity.HasMany(p => p.UsuariosEmpresa) 
                    .WithMany(u => u.Papeis)
                    .UsingEntity(j => j.ToTable("usuario_empresa_papel"));

                entity.HasMany(p => p.Permissoes)
                    .WithMany(p => p.Papeis)
                    .UsingEntity(j => j.ToTable("papel_permissao"));
            });

            modelBuilder.Entity<Permissao>(entity =>
            {
                entity.ToTable("permissao");
                entity.Property(p => p.Recurso).IsRequired();
                entity.Property(p => p.Acao).IsRequired();
                entity.HasIndex(p => new { p.Recurso, p.Acao }).IsUnique();
            });
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void UseSnakeCase(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToSnakeCase());

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToSnakeCase());
                }

                foreach (var key in entity.GetKeys())
                {
                    key.SetName(key.GetName().ToSnakeCase());
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(key.GetConstraintName().ToSnakeCase());
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(index.GetDatabaseName().ToSnakeCase());
                }
            }
        }

        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var builder = new System.Text.StringBuilder();
            for (var i = 0; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]))
                {
                    if (i > 0) builder.Append("_");
                    builder.Append(char.ToLowerInvariant(input[i]));
                }
                else
                {
                    builder.Append(input[i]);
                }
            }

            return builder.ToString();
        }
    }
}