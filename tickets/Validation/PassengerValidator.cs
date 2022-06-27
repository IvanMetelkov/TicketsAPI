using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using tickets.DTO;

namespace tickets.Validation
{
    public class PassengerValidator : AbstractValidator<PassengerDTO>
    {
        public PassengerValidator()
        {
            RuleFor(p => p.Name).NotEmpty()
                .Must(s => s.All(char.IsLetter));

            RuleFor(p => p.Surname).NotEmpty()
                .Must(s => s.All(char.IsLetter));

            RuleFor(p => p.Patronymic).NotEmpty()
                .Must(s => s.All(char.IsLetter));

            RuleFor(p => p.DocType).NotEmpty()
                .Must(s => s.All(char.IsDigit));

            RuleFor(p => p.DocNumber).NotEmpty()
                .Must(s => s.All(char.IsDigit))
                .Length(10).When(s => s.DocType == "00", ApplyConditionTo.CurrentValidator);

            RuleFor(p => p.Birthdate).NotEmpty()
                .Must(s => DateTime.TryParseExact(s, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _));

            RuleFor(p => p.Gender).Must(x => new List<string>() { "M", "F" }.Contains(x));

            RuleFor(p => p.PassengerType).NotEmpty()
                .Must(s => s.All(char.IsLetter));

            RuleFor(p => p.TicketNumber).NotEmpty()
                .Length(13)
                .Must(s => s.All(char.IsDigit));

            RuleFor(p => p.TicketType).GreaterThan(0);
        }
    }
}
