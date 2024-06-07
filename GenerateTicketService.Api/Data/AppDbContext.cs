using GenerateTicketService.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GenerateTicketService.Api.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<TicketInfo> TicketInfo { get; set; }

    }
}
