using Microsoft.EntityFrameworkCore;
using ParkingManager.Model.Entities;

namespace ParkingManager
{
    public class ParkingManagerDbContext : DbContext
    {
        public ParkingManagerDbContext(DbContextOptions<ParkingManagerDbContext> options) : base(options)
        {
        }

        public DbSet<ParkingRule> PackingRules { get; set; }
        public DbSet<ParkingTicket> PackingTickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ParkingRule>().HasKey(r => r.Id);
            modelBuilder.Entity<ParkingTicket>().HasKey(t => t.Id);

        }
    }
}
