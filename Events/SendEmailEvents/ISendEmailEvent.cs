using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Events.SendEmailEvents
{
    //This event will be published when ticket information is generated.
    //If the logic of age is less than 80, this event will publish.
    public interface ISendEmailEvent
    {
        public Guid TicketId { get; }
        public string Title { get; }
        public string Email { get; }
        public DateTime RequireDate { get; }
        public int Age { get; }
        public string Location { get; }
        public string TicketNumber { get; }
    }
}
