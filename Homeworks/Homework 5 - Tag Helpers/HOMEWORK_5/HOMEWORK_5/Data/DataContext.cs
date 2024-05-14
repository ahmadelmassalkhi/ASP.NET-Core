using CVInfoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CVInfoApp.Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<CV> CVs { get; set; }
	}
}
