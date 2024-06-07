using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Developers.Models
{
    public partial class MercyDeveloperContext : DbContext
    {
        public MercyDeveloperContext()
        {
        }

        public MercyDeveloperContext(DbContextOptions<MercyDeveloperContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Descripcionservicio> Descripcionservicios { get; set; }
        public virtual DbSet<Recepcionequipo> Recepcionequipos { get; set; }
        public virtual DbSet<Servicio> Servicios { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Aquí debes configurar tu cadena de conexión
                optionsBuilder.UseMySql("server=localhost;port=3306;database=mercy_developer;uid=root", new MySqlServerVersion(new Version(8, 0, 21)));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente).HasName("PRIMARY");

                entity.ToTable("cliente");

                entity.Property(e => e.IdCliente)
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_Cliente");
                entity.Property(e => e.Apellido).HasMaxLength(45);
                entity.Property(e => e.Correo).HasMaxLength(45);
                entity.Property(e => e.Direccion).HasMaxLength(45);
                entity.Property(e => e.Estado).HasMaxLength(45);
                entity.Property(e => e.Nombre).HasMaxLength(45);
                entity.Property(e => e.Telefono).HasMaxLength(45);
            });

            modelBuilder.Entity<Descripcionservicio>(entity =>
            {
                entity.HasKey(e => e.IdDs).HasName("PRIMARY");

                entity.ToTable("descripcionservicio");

                entity.HasIndex(e => e.IdServicio, "ID_Servicio_idx");

                entity.Property(e => e.IdDs)
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_DS");
                entity.Property(e => e.IdServicio)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_Servicio");
                entity.Property(e => e.Nombre).HasMaxLength(45);

                entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Descripcionservicios)
                    .HasForeignKey(d => d.IdServicio)
                    .HasConstraintName("fk_ID_Servicio");
            });

            modelBuilder.Entity<Recepcionequipo>(entity =>
            {
                entity.HasKey(e => e.IdRe).HasName("PRIMARY");

                entity.ToTable("recepcionequipo");

                entity.HasIndex(e => e.IdCliente, "ID_Cliente_idx");

                entity.HasIndex(e => e.IdServicio, "ID_Servicio_idx");

                entity.Property(e => e.IdRe)
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_RE");
                entity.Property(e => e.Accesorio).HasMaxLength(400);
                entity.Property(e => e.CanpacidadRam).HasColumnType("int(11)");
                entity.Property(e => e.Fecha).HasColumnType("datetime");
                entity.Property(e => e.Grafico).HasMaxLength(60);
                entity.Property(e => e.IdCliente)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_Cliente");
                entity.Property(e => e.IdServicio)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_Servicio");
                entity.Property(e => e.MarcaPc).HasMaxLength(60);
                entity.Property(e => e.MoledoPc).HasMaxLength(60);
                entity.Property(e => e.Nserie)
                    .HasMaxLength(100)
                    .HasColumnName("NSerie");
                entity.Property(e => e.TipoAlmacenamiento).HasMaxLength(60);
                entity.Property(e => e.TipoGpu).HasColumnType("int(11)");
                entity.Property(e => e.TipoPc).HasColumnType("int(11)");

                entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Recepcionequipos)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("ID_Cliente");

                entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Recepcionequipos)
                    .HasForeignKey(d => d.IdServicio)
                    .HasConstraintName("ID_Servicio");
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.HasKey(e => e.IdServicio).HasName("PRIMARY");

                entity.ToTable("servicio");

                entity.HasIndex(e => e.IdUsuario, "ID_Usuario_idx");

                entity.Property(e => e.IdServicio)
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_Servicio");
                entity.Property(e => e.IdUsuario)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID_Usuario");
                entity.Property(e => e.Nombre).HasMaxLength(100);
                entity.Property(e => e.Precio).HasColumnType("int(11)");
                entity.Property(e => e.Sku).HasMaxLength(45);

                entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Servicios)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("ID_Usuario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("usuario");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");
                entity.Property(e => e.Apellido).HasMaxLength(45);
                entity.Property(e => e.Correo).HasMaxLength(65);
                entity.Property(e => e.Nombre).HasMaxLength(45);
                entity.Property(e => e.Password).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
