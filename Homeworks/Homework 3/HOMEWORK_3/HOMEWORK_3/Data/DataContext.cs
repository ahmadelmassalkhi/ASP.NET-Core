using HOMEWORK_3.Models;
using Microsoft.EntityFrameworkCore;

namespace HOMEWORK_3.Data
{


    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        // Tables
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }



}
