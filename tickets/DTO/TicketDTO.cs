using System.Collections.Generic;

namespace tickets.DTO
{
    public class TicketDTO : IRequestDTO
    {
        public string OperationType { get; set; }

        public string OperationTime { get; set; }

        public string OperationPlace { get; set; }

        public PassengerDTO Passenger { get; set; }

        public IEnumerable<SegmentDTO> Routes { get; set; }
    }
}
