using tickets.DTO;
using FluentValidation;

namespace tickets.Validation
{
    public class SaleValidator : RequestValidator<TicketDTO>
    {
        public SaleValidator(IValidator<PassengerDTO> passengerValidator, IValidator<SegmentDTO> segmentValidator) : base()
        {
            RuleFor(r => r.Passenger).SetValidator(passengerValidator);
            RuleForEach(r => r.Routes).SetValidator(segmentValidator);
        }
    }
}
