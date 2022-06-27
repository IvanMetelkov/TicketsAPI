using tickets.DTO;
using FluentValidation;
using System.Linq;

namespace tickets.Validation
{
    public class RefundValidator : RequestValidator<RefundDTO>
    {
        public RefundValidator() : base()
        {
            RuleFor(r => r.TicketNumber).NotEmpty()
                .Length(13)
                .Must(s => s.All(char.IsDigit));
        }
    }
}
