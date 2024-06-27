using System;
using System.Collections.Generic;
using Entregas.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Entregas.API.Data;

public partial class EntregasContext : DbContext
{
    public EntregasContext()
    {
    }

    public EntregasContext(DbContextOptions<EntregasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetSystem> AspNetSystems { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<ConceptoPago> ConceptoPagos { get; set; }

    public virtual DbSet<Cuentum> Cuenta { get; set; }

    public virtual DbSet<EstatusPedido> EstatusPedidos { get; set; }

    public virtual DbSet<Local> Locals { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<TipoCuentum> TipoCuenta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-540B3V8;Initial Catalog=Entregas;Persist Security Info=True;User ID=sa;Password=sql2;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);

            entity.HasOne(d => d.Sistema).WithMany(p => p.AspNetRoles)
                .HasForeignKey(d => d.SistemaId)
                .HasConstraintName("FK_AspNetRoles_AspNetSystem");
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.Property(e => e.RoleId).HasMaxLength(450);

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetSystem>(entity =>
        {
            entity.ToTable("AspNetSystem");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Clave).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(500);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<ConceptoPago>(entity =>
        {
            entity.ToTable("ConceptoPago");

            entity.Property(e => e.ConceptoPagoId).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasMaxLength(500);
        });

        modelBuilder.Entity<Cuentum>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Apellidos).HasMaxLength(500);
            entity.Property(e => e.Avatar).HasMaxLength(500);
            entity.Property(e => e.Correo).HasMaxLength(50);
            entity.Property(e => e.Cuenta).HasMaxLength(50);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(500);
            entity.Property(e => e.Telefono).HasMaxLength(50);

            entity.HasOne(d => d.TipoCuenta).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.TipoCuentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cuenta_TipoCuenta");
        });

        modelBuilder.Entity<EstatusPedido>(entity =>
        {
            entity.ToTable("EstatusPedido");

            entity.Property(e => e.EstatusPedidoId).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<Local>(entity =>
        {
            entity.ToTable("Local");

            entity.Property(e => e.LocalId).ValueGeneratedNever();
            entity.Property(e => e.Avatar).HasMaxLength(500);
            entity.Property(e => e.Direccion).HasMaxLength(500);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Lat).HasMaxLength(50);
            entity.Property(e => e.Lon).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(500);
            entity.Property(e => e.Telefono).HasMaxLength(50);

            entity.HasOne(d => d.Cuenta).WithMany(p => p.Locals)
                .HasForeignKey(d => d.CuentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Local_Cuenta");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.ToTable("MetodoPago");

            entity.Property(e => e.MetodoPagoId).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasMaxLength(500);
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.ToTable("Pago");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Comprobante).HasMaxLength(500);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Importe).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Observaciones).HasMaxLength(500);
            entity.Property(e => e.Referencia).HasMaxLength(500);

            entity.HasOne(d => d.ConceptoPago).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.ConceptoPagoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_ConceptoPago");

            entity.HasOne(d => d.Cuenta).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.CuentaId)
                .HasConstraintName("FK_Pago_Cuenta");

            entity.HasOne(d => d.MetodoPag).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.MetodoPagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pago_MetodoPago");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.ToTable("Pedido");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Comision).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Destino).HasMaxLength(500);
            entity.Property(e => e.DestinoLat).HasMaxLength(50);
            entity.Property(e => e.DestinoLon).HasMaxLength(50);
            entity.Property(e => e.Distancia).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.FechaAsignado).HasColumnType("datetime");
            entity.Property(e => e.FechaCancelacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaEntrega).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Folio).ValueGeneratedOnAdd();
            entity.Property(e => e.Observaciones).HasMaxLength(500);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.EstatusPedido).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.EstatusPedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_EstatusPedido");

            entity.HasOne(d => d.Local).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.LocalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_Local");

            entity.HasOne(d => d.Repartidor).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.RepartidorId)
                .HasConstraintName("FK_Pedido_Cuenta");
        });

        modelBuilder.Entity<TipoCuentum>(entity =>
        {
            entity.HasKey(e => e.TipoCuentaId);

            entity.Property(e => e.TipoCuentaId).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
