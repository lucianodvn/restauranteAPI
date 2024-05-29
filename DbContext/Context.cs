using Microsoft.EntityFrameworkCore;
using Restaurante.API.Models;

namespace Restaurante.API.DbConext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Status> Estado { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<RestauranteApp> Restaurantes { get; set; }
    }
}