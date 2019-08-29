using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderApplication.Models
{
    public class DrinkMenuDbContext : DbContext
    {
        public DrinkMenuDbContext(DbContextOptions<DrinkMenuDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Drinks> Drinks { get; set; }
    }
}
