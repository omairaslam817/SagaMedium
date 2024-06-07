using AutoMapper;
using Events.TicketEvents;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using TicketService.Api.DTO;
using TicketService.Api.Models;
using TicketService.Api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketServices _ticketServices;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _bus;
        public TicketController(ITicketServices ticketServices, IMapper mapper, IPublishEndpoint bus)
        {
            _ticketServices = ticketServices;
            _mapper = mapper;
            _bus = bus;
        }
        // GET: api/<TicketController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TicketController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TicketController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddTicketDTO addTicketDTO)
        {
            var mapModel = _mapper.Map<Ticket>(addTicketDTO);
            var res = await _ticketServices.AddTicket(mapModel);
            if (res is not null)
            {
                // map model to the DTO and pass the DTO object to the bus queue
                var mapResult = _mapper.Map<ResponseTicketDTO>(res);
                //Send to Bus
               // var endPoint = await _bus.Publish(new Uri("queue:" + MessageBrokers.RabbitMQQueues.SagaBusQueue));
                await _bus.Publish<IGETValueEvent>(new 
                {
                    TicketId = Guid.Parse(mapResult.TicketId),
                    Title = mapResult.Title,
                    Email = mapResult.Email,
                    RequireDate = mapResult.RequireDate,
                    Age = mapResult.Age,
                    Location = mapResult.Location
                });
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest();
        }

        // PUT api/<TicketController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TicketController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
