namespace TicketService.Api.DTO
{

    /// <summary>
    /// pass the response to the client or send a specific object to the bus
    /// </summary>
    public class ResponseTicketDTO
    {
        public string TicketId { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public DateTime RequireDate { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public string TicketNumber { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
