using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<bool> RefundTicketAsync(string ticketNumber)
        {
            int updatedRowsCount = await _context.Database.ExecuteSqlInterpolatedAsync(@$"UPDATE ""segments"" SET refunded = 'true' 
                WHERE ticket_number = {ticketNumber} AND refunded = 'false'");
            
            return updatedRowsCount > 0;
        }
    }
}
