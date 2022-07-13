using System.Threading.Tasks;
using tickets.DTO;

namespace tickets.DAL
{
    public interface ISegmentRepository
    {
        Task AddSegmentsAsync(TicketDTO ticket);
        Task RefundTicketAsync(RefundDTO refund);
    }
}
