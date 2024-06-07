using Microsoft.EntityFrameworkCore;
using SagaStateMachine.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SagaService.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TicketStateData>().HasKey(x => x.CorrelationId);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<TicketStateData> TicketStateData { get; set; }
    }
}
