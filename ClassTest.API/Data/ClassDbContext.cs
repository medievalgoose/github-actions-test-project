using ClassTest.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassTest.API.Data
{
    public class ClassDbContext : DbContext
    {
        public ClassDbContext(DbContextOptions<ClassDbContext> options) : base(options)
        {
        }

        public DbSet<Classe> Classes { get; set; }
    }
}
