using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.TicketEvents
{
    public class ICancelGenerateTicketEvent
    {
        public Guid TicketId { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public DateTime RequireDate { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public string TicketNumber { get; set; }
    }
}
