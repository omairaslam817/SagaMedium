using AutoMapper;
using Events.TicketEvents;
using GenerateTicketService.Api.Models;

namespace GenerateTicketService.Api.Common.mapping
{
    public class TicketInfoMapping: Profile
    {
        public TicketInfoMapping()
        {
            CreateMap<IGenerateTicketEvent, TicketInfo>();
        }
    }
}
