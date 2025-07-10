using Microsoft.EntityFrameworkCore;
using LecturasApi.Models;

namespace LecturasApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Medidor> Medidores { get; set; }
        public DbSet<Lectura> Lecturas { get; set; }
    }
}
