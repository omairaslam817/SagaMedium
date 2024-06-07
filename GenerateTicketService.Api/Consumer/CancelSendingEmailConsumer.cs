using Events.SendEmailEvents;
using Events.TicketEvents;
using GenerateTicketService.Api.Services;
using MassTransit;

namespace GenerateTicketService.Api.Consumer
{
    public class CancelSendingEmailConsumer : IConsumer<ICancelSendEmailEvent>
    {

        /// <summary>
        /// As shown in the SendEmailConsumer, if the location is equal to London an ICancelSendEmailEvent will occur.
        /// So, a consumer will be required in the GenerateTicket project to consume the cancelation of sending email and remove its data like the TicketService. Therefore, in the GenerateTicket project and the Consumers folder,
        /// create a new class and call it CancelSendingEmailConsumer and add the following code.
        /// </summary>
        private readonly ITicketInfoService _ticketInfoService;
        private readonly ILogger<CancelSendingEmailConsumer> _logger;
        public CancelSendingEmailConsumer(ITicketInfoService ticketInfoService, ILogger<CancelSendingEmailConsumer> logger)
        {
            _ticketInfoService = ticketInfoService;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<ICancelSendEmailEvent> context)
        {
            var data = context.Message;
            if (data is not null)
            {
                var res = _ticketInfoService.RemoveTicketInfo(data.TicketId.ToString());
                if (res is true)
                {
                    await context.Publish<ICancelGenerateTicketEvent>(new
                    {
                        TicketId = data.TicketId,
                        Title = data.Title,
                        Email = data.Email,
                        RequireDate = data.RequireDate,
                        Age = data.Age,
                        Location = data.Location
                    });
                    _logger.LogInformation("The message has been sent to the ICancelGenerateTicketEvent in the TicketService");

                }
                else
                {
                    _logger.LogInformation("Remove Ticket Info Failed!!!");
                }
            }
        }
    }
}
