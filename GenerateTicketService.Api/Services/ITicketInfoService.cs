using GenerateTicketService.Api.Models;

namespace GenerateTicketService.Api.Services
{
    public interface ITicketInfoService
    {
        Task<TicketInfo> AddTicketInfo(TicketInfo ticketInfo);
        bool RemoveTicketInfo(string TicketId);
    }
}
