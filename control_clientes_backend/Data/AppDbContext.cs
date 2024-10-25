using control_clientes_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace control_clientes_backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
    }
}
