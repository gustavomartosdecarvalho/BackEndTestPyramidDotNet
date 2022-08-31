using Alimentacao_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Alimentacao_App.DbContexts
{
    public class AnimalContext : DbContext
    {
        public AnimalContext(DbContextOptions<AnimalContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Animal> Animals { get; set; }
    }
}