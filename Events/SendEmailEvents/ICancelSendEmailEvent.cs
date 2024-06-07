using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.SendEmailEvents
{

    /// <summary>
    /// This event will be published when sending email logic failed. In this scenario,
    /// the location should not be London. So, if the location is London, this event will publish.
    /// </summary>
    public interface ICancelSendEmailEvent
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
