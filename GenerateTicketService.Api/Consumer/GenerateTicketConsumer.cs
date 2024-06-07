using AutoMapper;
using Events.SendEmailEvents;
using Events.TicketEvents;
using GenerateTicketService.Api.Models;
using GenerateTicketService.Api.Services;
using MassTransit;

namespace GenerateTicketService.Api.Consumer
{

    /// <summary>
    /// As shown in the above consumer, this class listening IGenerateTicketEvent and when this event occurred,
    /// this class retrieve the message.
    /// </summary>
    public class GenerateTicketConsumer : IConsumer<IGenerateTicketEvent>
    {
        private readonly ITicketInfoService _ticketInfoService;
        private readonly ILogger<GenerateTicketConsumer> _logger;
        private readonly IMapper _mapper;

        //As shown this consumer is listening to IGenerateTicketEvent
        //But But Ticket Service publish its message to IAddTicketEvent
        //Here State Mechiene will transform IAddTicketEvent to IGenerateTicketEvent
        public GenerateTicketConsumer(ITicketInfoService ticketInfoService,
            ILogger<GenerateTicketConsumer> logger,
            IMapper mapper)
        {
            _ticketInfoService = ticketInfoService;
            _logger = logger;
            _mapper = mapper;
        }
        
        public async Task Consume(ConsumeContext<IGenerateTicketEvent> context)
        {
            try
            {
                var data = context.Message;
                if (data is not null)
                {
                    //Check if Age is 80 or above
                    if (data.Age < 80)
                    {
                        //Store message
                        //Use Mapper or use a ticketinfo object directly
                        var mapModel = _mapper.Map<TicketInfo>(data);
                        var res = await _ticketInfoService.AddTicketInfo(mapModel);
                        if (res is not null)
                        {

                            await context.Publish<ISendEmailEvent>(new
                            {
                                TicketId = data.TicketId,
                                Title = data.Title,
                                Email = data.Email,
                                RequireDate = data.RequireDate,
                                Age = data.Age,
                                Location = data.Location,
                                TicketNumber = res.TicketNumber
                            });
                            _logger.LogInformation($"Message sent == TicketId is {data.TicketId}");

                        }
                    }
                    else
                    {
                        //This section will return message to CancelEvent
                        /// <summary>
                        /// the cancel event is when the GeneratedTicket service denies an
                        /// incoming message that the age is more than 80
                        /// </summary>


                        await context.Publish<ICancelGenerateTicketEvent>(new
                        {
                            TicketId = data.TicketId,
                            Title = data.Title,
                            Email = data.Email,
                            RequireDate = data.RequireDate,
                            Age = data.Age,
                            Location = data.Location
                        });
                        _logger.LogInformation($"Message canceled== TicketId is {data.TicketId}");

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
