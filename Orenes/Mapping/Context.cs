using Microsoft.EntityFrameworkCore;
using Orenes.Models;

namespace Orenes.Mapping
{
    public class Context : DbContext
    {

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }


        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>()
           .HasMany(c => c.Pedidos)
           .WithOne(p => p.Cliente)
           .HasForeignKey(p => p.ClienteId)
           .IsRequired();

            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Ubicaciones)
                .WithOne(u => u.Pedido)
                .HasForeignKey(u => u.PedidoId)
                .IsRequired();

            modelBuilder.Entity<Vehiculo>()
                .HasMany(v => v.Ubicaciones)
                .WithOne(u => u.Vehiculo)
                .HasForeignKey(u => u.VehiculoId)
                .IsRequired();


            modelBuilder.Entity<Ubicacion>()
            .HasOne(u => u.Vehiculo)
            .WithMany(v => v.Ubicaciones)
            .HasForeignKey(u => u.VehiculoId)
            .IsRequired();

            modelBuilder.Entity<Ubicacion>()
            .HasOne(u => u.Pedido)
            .WithMany(p => p.Ubicaciones)
            .HasForeignKey(u => u.PedidoId)
            .IsRequired();

        }
    }
}
