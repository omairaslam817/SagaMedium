using TicketService.Api.Data;
using TicketService.Api.Models;

namespace TicketService.Api.Services
{
    public class TicketServices: ITicketServices
    {
        private readonly AppDbContext _dbContext;

        public TicketServices(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Ticket> AddTicket(Ticket ticket)
        {
            if (ticket is not null)
            {
                await _dbContext.Tickets.AddAsync(ticket);
                await _dbContext.SaveChangesAsync();
            }
            return ticket;
        }

        public bool DeleteTicket(string TicketId)
        {
            var ticketObj = _dbContext.Tickets.FirstOrDefault(t => t.TicketId == TicketId);
            if (ticketObj is not null)
            {
                _dbContext.Tickets.Remove(ticketObj);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
