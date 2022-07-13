using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using tickets.DAL;
using tickets.DTO;

namespace tickets.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1.0")]
    [RequestSizeLimit(2048)]
    public class SegmentsController : ControllerBase
    {
        private readonly ISegmentRepository _repository;
        public SegmentsController(ISegmentRepository repository)
        {
            _repository = repository;
        }

        [Route("sale")]
        [HttpPost]
        public async Task<ActionResult> RegisterSaleAsync(TicketDTO ticket)
        {
            await _repository.AddSegmentsAsync(ticket);

            return Ok();
        }

        [Route("refund")]
        [HttpPost]
        public async Task<ActionResult> RefundTicketAsync(RefundDTO refund)
        {
            await _repository.RefundTicketAsync(refund);

            return Ok();
        }
    }
}
