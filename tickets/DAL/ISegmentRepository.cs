using System.Collections.Generic;
using System.Threading.Tasks;
using tickets.Model;

namespace tickets.DAL
{
    public interface ISegmentRepository
    {
        Task AddSegmentsAsync(IEnumerable<Segment> segments);
    }
}
