using FluentValidation;
using System;
using System.Globalization;
using System.Linq;
using tickets.DTO;

namespace tickets.Validation
{
    public class SegmentValidator : AbstractValidator<SegmentDTO>
    {
        public SegmentValidator()
        {
            RuleFor(seg => seg.AirlineCode).NotEmpty()
                .Must(s => s.All(char.IsLetter));

            RuleFor(seg => seg.FlightNum).GreaterThan(0);

            RuleFor(seg => seg.DepartPlace).NotEmpty()
                .Must(s => s.All(char.IsLetter));

            RuleFor(seg => seg.DepartDatetime).NotEmpty()
                .Must(s => DateTime.TryParseExact(s, "yyyy-MM-ddTHH:mmzzz", CultureInfo.InvariantCulture, DateTimeStyles.None, out _));

            RuleFor(seg => seg.ArrivePlace).NotEmpty()
                .Must(s => s.All(char.IsLetter));

            RuleFor(seg => seg.ArriveDatetime).NotEmpty()
                .Must(s => DateTime.TryParseExact(s, "yyyy-MM-ddTHH:mmzzz", CultureInfo.InvariantCulture, DateTimeStyles.None, out _));

            RuleFor(seg => seg.PnrId).NotEmpty()
                .Must(s => s.All(char.IsLetter));
        }
    }
}
