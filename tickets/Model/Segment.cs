using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace tickets.Model
{
    [Index(nameof(TicketNumber), nameof(SerialNumber), IsUnique = true)]
    public class Segment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AirlineCode { get; set; }

        [Required]
        public int FlightNum { get; set; }

        [Required]
        public string DepartPlace { get; set; }

        [Required]
        public DateTimeOffset DepartDatetime { get; set; }

        [Required]
        public short DepartTimeTimezone { get; set; }

        [Required]
        public string ArrivePlace { get; set; }

        [Required]
        public DateTimeOffset ArriveDatetime { get; set; }

        [Required]
        public short ArriveTimeTimezone { get; set; }

        [Required]
        public string PnrId { get; set; }

        [Required]
        public string TicketNumber { get; set; }

        [Required]
        public int SerialNumber { get; set; }

        [Required]
        public bool Refunded { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Patronymic { get; set; }

        [Required]
        public string DocType { get; set; }

        [Required]
        public string DocNumber { get; set; }

        [Required]
        public string Gender { get; set; }
    }
}
