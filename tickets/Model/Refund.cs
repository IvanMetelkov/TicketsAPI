using System;

namespace tickets.Model
{
    public class Refund
    {
        public string TicketNumber { get; set; }

        public string OperationPlace { get; set; }

        public DateTimeOffset OperationTime { get; set; }

        public short OperationTimeTimezone { get; set; }
    }
}
