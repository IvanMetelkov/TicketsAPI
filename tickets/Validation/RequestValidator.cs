using FluentValidation;
using System;
using System.Globalization;
using System.Linq;
using tickets.DTO;

namespace tickets.Validation
{
    public class RequestValidator<T> : AbstractValidator<T> where T : IRequestDTO
    {
        public RequestValidator()
        {
            RuleFor(r => r.OperationType).NotEmpty()
                .Must(s => s.All(char.IsLetter));

            RuleFor(r => r.OperationTime).NotEmpty()
                .Must(s => DateTime.TryParseExact(s, "yyyy-MM-ddTHH:mmzzz", CultureInfo.InvariantCulture, DateTimeStyles.None, out _));

            RuleFor(r => r.OperationPlace).NotEmpty()
                .Must(s => s.All(char.IsLetter));
        }
    }
}
