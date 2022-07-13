using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using tickets.DTO;
using tickets.Exceptions;
using tickets.Model;

namespace tickets.DAL
{
    public class SegmentRepository : ISegmentRepository
    {
        private readonly SegmentContext _context;
        private readonly IMapper _mapper;

        public SegmentRepository(SegmentContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddSegmentsAsync(TicketDTO ticket)
        {
            IEnumerable<Segment> segments = _mapper.Map<IEnumerable<Segment>>(ticket.Routes);
            foreach (Segment segment in segments)
            {
                _mapper.Map(ticket, segment);
                _mapper.Map(ticket.Passenger, segment);
            }

            const string lockTimeout = "SET lock_timeout = '120s'";

            await _context.Database.OpenConnectionAsync();
            await _context.Database.ExecuteSqlRawAsync(lockTimeout);
            //_context.Database.SetCommandTimeout(0);
            int serialCounter = 1;

            foreach (var segment in segments)
            {
                segment.SerialNumber = serialCounter++;
                await _context.Segments.AddAsync(segment);
            }

            await _context.SaveChangesAsync();
            await _context.Database.CloseConnectionAsync();
        }
    
        public async Task RefundTicketAsync(RefundDTO refundDTO)
        {
            Refund refund = _mapper.Map<Refund>(refundDTO);

            const string lockTimeout = "SET lock_timeout = '120s'";

            await _context.Database.OpenConnectionAsync();
            await _context.Database.ExecuteSqlRawAsync(lockTimeout);

            int updatedRowsCount = await _context.Database.ExecuteSqlInterpolatedAsync(@$"UPDATE ""segments"" SET refunded = 'true',
                operation_place = {refund.OperationPlace}, operation_time = {refund.OperationTime}, operation_time_timezone = {refund.OperationTimeTimezone}
                WHERE ticket_number = {refund.TicketNumber} AND refunded = 'false'");
            
            if (updatedRowsCount == 0)
            {
                throw new RefundException("could not issue a refund");
            }
        }
    }
}
