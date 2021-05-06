using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Models.SupportTicket
{
    public class SupportTicket
    {
        public int Id { get; set; }
        public long TicketId { get; set; }
        public string TicketReference { get; set; }
        public string ProductIssue { get; set; }
        public string VersionOrPackage { get; set; }
        public string Comment { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; } // what user opened this support ticket...
        public Status Status { get; set; }
        public DateTime DateOpened { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateClosed { get; set; }

        Random rand = new Random();

        public SupportTicket()
        {
            TicketId = (long)(Math.Floor(rand.NextDouble() * 4_00_000_000L + 1_000_000_000L));
            TicketReference = Convert.ToString($":ref:_{Guid.NewGuid().ToString().Replace("-", "_").Substring(2, 24)}:ref");
        }

    }

    public enum Status
    {
        Closed,
        Open,
        Updated,
        Archived,
    }
}
