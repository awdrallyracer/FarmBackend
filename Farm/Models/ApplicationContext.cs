using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farm.Models.DbModels;


namespace Farm.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            // Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public DbSet<Animal> Animals { get; set; } 
        public DbSet<Injection> Injections { get; set; }
        public DbSet<Barn> Barns { get; set; }
        public DbSet<Care> Cares { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Backup> Backups { get; set; }
    }
}
