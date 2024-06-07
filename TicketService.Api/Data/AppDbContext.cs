﻿using Microsoft.EntityFrameworkCore;
using TicketService.Api.Models;

namespace TicketService.Api.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
