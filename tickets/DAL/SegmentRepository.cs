using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using tickets.Model;


namespace tickets.DAL
{
    public class SegmentRepository : ISegmentRepository
    {
        private readonly SegmentContext _context;

        public SegmentRepository(SegmentContext context)
        {
            _context = context;
        }

        public async Task AddSegmentsAsync(IEnumerable<Segment> segments)
        {
            const string lockTimeout = "SET lock_timeout = '120s'";

            await _context.Database.OpenConnectionAsync();
            await _context.Database.ExecuteSqlRawAsync(lockTimeout);
            int serialCounter = 1;

            foreach (var segment in segments)
            {
                segment.SerialNumber = serialCounter++;
                await _context.Segments.AddAsync(segment);
            }

            await _context.SaveChangesAsync();
            await _context.Database.CloseConnectionAsync();

            /*var transaction = await _context.Database.BeginTransactionAsync();
            await _context.Database.ExecuteSqlRawAsync("SET LOCAL lock_timeout = '120s'");

            int serialCounter = 1;
            foreach (var segment in segments)
            {

                segment.SerialNumber = serialCounter;
                await _context.Database.ExecuteSqlInterpolatedAsync(@$"INSERT INTO ""segments""(airline_code, flight_num, depart_place,
                    depart_datetime, depart_time_timezone, arrive_place, arrive_datetime, arrive_time_timezone, pnr_id, 
                    ticket_number, serial_number, refunded, name, surname, patronymic, doc_type, doc_number, gender) 
                    VALUES ({segment.AirlineCode}, {segment.FlightNum}, 
                    {segment.DepartPlace}, {segment.DepartDatetime}, {segment.DepartTimeTimezone}, {segment.ArrivePlace}, {segment.ArriveDatetime}, 
                    {segment.ArriveTimeTimezone}, {segment.PnrId}, {segment.TicketNumber}, {segment.SerialNumber}, {segment.Refunded}, 
                    {segment.Name}, {segment.Surname}, {segment.Patronymic}, {segment.DocType}, {segment.DocNumber}, {segment.Gender})");
                serialCounter++;
            }

            await transaction.CommitAsync();*/
        }
    }
}
