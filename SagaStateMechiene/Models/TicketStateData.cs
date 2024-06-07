using MassTransit;

namespace SagaStateMachine.Models
{

    /// <summary>
    /// , this class represents all the data that is required to determine the state of the request received by the SagaStateMachine.
    /// The CorrelationId is mandatory property acting as the key value of the data.
    /// </summary>
    public class TicketStateData: SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public DateTime TicketCreatedDate { get; set; }
        public DateTime TicketCancelDate { get; set; }
        public Guid TicketId { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public int Age { get; set; }
        public string TicketNumber { get; set; }
    }
}
