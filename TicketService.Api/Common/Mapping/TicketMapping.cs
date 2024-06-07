using AutoMapper;
using TicketService.Api.DTO;
using TicketService.Api.Models;

namespace TicketService.Api.Common.Mapping
{
    public class TicketMapping: Profile
    {
        public TicketMapping() {
            CreateMap<AddTicketDTO, Ticket>();
            CreateMap<Ticket, ResponseTicketDTO>();
        }

    }
}
