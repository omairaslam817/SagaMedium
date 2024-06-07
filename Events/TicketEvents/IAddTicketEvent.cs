using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.TicketEvents
{
    public class IAddTicketEvent
    {
        public Guid TicketId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime RequireDate { get; set; }
        public int Age { get; set; }
        public string Location { get; set; } = string.Empty;
        public string TicketNumber { get; set; } = string.Empty;
    }
}
