using Events.TicketEvents;
using MassTransit;
using TicketService.Api.Services;

namespace TicketService.Api.Consumers
{

    /// <summary>
    /// As shown in the GenerateTicketConsumer file, this consumer is listening to the IGenerateTicketEvent,
    /// however, when a ticket is created, IAddTicketEvent occurred. So,
    /// The IAddTicketEvent is transformed by the saga state machine. Furthermore,
    /// it will be sent a message to the ISendEmailEvent when the age is less than 80.
    /// </summary>
    public class GenerateTicketCancelConsumer : IConsumer<ICancelGenerateTicketEvent>
    {
        private readonly ITicketServices _ticketServices;
        private readonly ILogger<GenerateTicketCancelConsumer> _logger;
        public GenerateTicketCancelConsumer(ITicketServices ticketServices,
            ILogger<GenerateTicketCancelConsumer> logger)
        {
            _ticketServices = ticketServices;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<ICancelGenerateTicketEvent> context) //GenerateTicketCancelConsumer  in program
        {
            var data = context.Message;
            if (data is not null)
            {
                var res = _ticketServices.DeleteTicket(data.TicketId.ToString());
                if (res is true)
                {
                    _logger.LogInformation("The Ticket has been removed successufully");
                }
                else
                {
                    _logger.LogInformation("Ticket removing operation Failed!!!");
                }
            }
        }
    }
}
