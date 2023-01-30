using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace webIntegradorSeptimo.Models
{
    public partial class context_base : DbContext
    {
        public context_base()
            : base("name=context_base")
        {
        }
        public virtual DbSet<administrador> administrador { get; set; }
        public virtual DbSet<categoria> categoria { get; set; }
        public virtual DbSet<cliente> cliente { get; set; }
        public virtual DbSet<cobertura> cobertura { get; set; }
        public virtual DbSet<configuracion> configuracion { get; set; }
        public virtual DbSet<detallePedido> detallePedido { get; set; }
        public virtual DbSet<devolucion> devolucion { get; set; }
        public virtual DbSet<estado> estado { get; set; }
        public virtual DbSet<imagenes_producto> imagenes_producto { get; set; }
        public virtual DbSet<logpedido> logpedido { get; set; }
        public virtual DbSet<oferta> oferta { get; set; }
        public virtual DbSet<pedido> pedido { get; set; }
        public virtual DbSet<producto> producto { get; set; }
        public virtual DbSet<recuperaClave> recuperaClave { get; set; }
        public virtual DbSet<reposicion> reposicion { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<administrador>()
                .Property(e => e.cedula)
                .IsUnicode(false);

            modelBuilder.Entity<administrador>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<administrador>()
                .Property(e => e.apellido)
                .IsUnicode(false);

            modelBuilder.Entity<administrador>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<administrador>()
                .Property(e => e.mail)
                .IsUnicode(false);

            modelBuilder.Entity<administrador>()
                .Property(e => e.usuario)
                .IsUnicode(false);

            modelBuilder.Entity<administrador>()
                .Property(e => e.contra)
                .IsUnicode(false);

            modelBuilder.Entity<categoria>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.apellido)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.celular)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.correo)
                .IsUnicode(false);

            modelBuilder.Entity<cliente>()
                .Property(e => e.contra)
                .IsUnicode(false);

            modelBuilder.Entity<cobertura>()
                .Property(e => e.ciudad)
                .IsUnicode(false);

            modelBuilder.Entity<configuracion>()
                .Property(e => e.about)
                .IsUnicode(false);

            modelBuilder.Entity<configuracion>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<configuracion>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<detallePedido>()
                .Property(e => e.precio)
                .HasPrecision(8, 2);

            modelBuilder.Entity<detallePedido>()
                .Property(e => e.subtotal)
                .HasPrecision(8, 2);

            modelBuilder.Entity<detallePedido>()
                .Property(e => e.envio)
                .HasPrecision(8, 2);

            modelBuilder.Entity<detallePedido>()
                .Property(e => e.iva)
                .HasPrecision(8, 2);

            modelBuilder.Entity<detallePedido>()
                .Property(e => e.total)
                .HasPrecision(8, 2);

            modelBuilder.Entity<estado>()
                .Property(e => e.estado1)
                .IsUnicode(false);

            modelBuilder.Entity<imagenes_producto>()
                .Property(e => e.referencia)
                .IsUnicode(false);

            modelBuilder.Entity<imagenes_producto>()
                .Property(e => e.size)
                .IsUnicode(false);

            modelBuilder.Entity<oferta>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<pedido>()
                .Property(e => e.comentario)
                .IsUnicode(false);

            modelBuilder.Entity<pedido>()
                .Property(e => e.total)
                .HasPrecision(8, 2);

            modelBuilder.Entity<pedido>()
                .Property(e => e.subtotal)
                .HasPrecision(8, 2);

            modelBuilder.Entity<pedido>()
                .Property(e => e.iva)
                .HasPrecision(8, 2);

            modelBuilder.Entity<pedido>()
                .Property(e => e.envio)
                .HasPrecision(8, 2);

            modelBuilder.Entity<producto>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<producto>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<producto>()
                .Property(e => e.pvp)
                .HasPrecision(8, 2);

            modelBuilder.Entity<producto>()
                .Property(e => e.stock)
                .HasPrecision(8, 2);

            modelBuilder.Entity<producto>()
                .Property(e => e.iva)
                .HasPrecision(8, 2);

            modelBuilder.Entity<recuperaClave>()
                .Property(e => e.validacion)
                .IsUnicode(false);

            modelBuilder.Entity<reposicion>()
                .Property(e => e.cantidad)
                .HasPrecision(8, 2);
        }
    }
}
