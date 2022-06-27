using System.Collections.Generic;
using System.Threading.Tasks;
using tickets.DTO;

namespace tickets.Validation
{
    public interface IValidator
    {
        public Task<bool> ValidateDTOAsync(IRequestDTO request);

        public bool ValidatePassenger(PassengerDTO passenger);

        public bool ValidateSegments(IEnumerable<SegmentDTO> segments);

        public bool ValidateSale(TicketDTO ticket);

        public bool ValidateRefund(RefundDTO refund);
    }
}