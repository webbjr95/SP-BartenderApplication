using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderApplication.Models
{
    public class OrdersPlacedDbContext : DbContext
    {
        public OrdersPlacedDbContext(DbContextOptions<OrdersPlacedDbContext> options)
               : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Orders> Orders { get; set; } 
    }
}
