﻿using Events.TicketEvents;
using MassTransit;

namespace TicketService.Api.Consumers
{
    public class GetValueConsumer : IConsumer<IGETValueEvent>
    {
        private readonly ILogger<GetValueConsumer> _logger;

        public GetValueConsumer(ILogger<GetValueConsumer> logger)
        {
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<IGETValueEvent> context)
        {
            var data = context.Message;
            if (data is not null)
            {
                //This section will publish message to  IAddTicketEvent altough the GenerateTikcet Service has consumer
                //that it will be listened on IAddTicketEvent
                await context.Publish<IAddTicketEvent>(new
                {
                    TicketId = data.TicketId,
                    Title = data.Title,
                    Email = data.Email,
                    RequireDate = data.RequireDate,
                    Age = data.Age,
                    Location = data.Location
                });
                _logger.LogInformation("a message has been received");

            }
        }
    }
}
