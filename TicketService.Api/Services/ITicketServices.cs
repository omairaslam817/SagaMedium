using TicketService.Api.Models;

namespace TicketService.Api.Services
{
    public interface ITicketServices
    {
        Task<Ticket> AddTicket(Ticket ticket);
        bool DeleteTicket(string TicketId);
    }
}
