﻿using System.ComponentModel.DataAnnotations;

namespace GenerateTicketService.Api.Models
{
    public class TicketInfo
    {
        [Key]
        public string TicketId { get; set; }
        public string Email { get; set; }
        public string TicketNumber { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
