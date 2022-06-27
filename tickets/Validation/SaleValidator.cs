using tickets.DTO;
using FluentValidation;

namespace tickets.Validation
{
    public class SaleValidator : RequestValidator<TicketDTO>
    {
        public SaleValidator() : base()
        {
            RuleFor(r => r.Passenger).NotNull();
            RuleFor(r => r.Routes).NotEmpty();
        }
    }
}
