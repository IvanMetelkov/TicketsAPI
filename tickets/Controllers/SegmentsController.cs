using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using tickets.DAL;
using tickets.DTO;
using tickets.Model;

namespace tickets.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/process")]
    [ApiVersion("1.0")]
    public class SegmentsController : ControllerBase
    {
        private readonly ISegmentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator _validator;
        public SegmentsController(ISegmentRepository repository, IMapper mapper, IValidator validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        [Route("sale")]
        [HttpPost]
        [RequestSizeLimit(2048)]
        public async Task<ActionResult> RegisterSale(TicketDTO ticket)
        {
            if (!await _validator.ValidateDTOAsync(ticket))
            {
                return BadRequest();
            }

            IEnumerable<Segment> segments = _mapper.Map<IEnumerable<Segment>>(ticket.Routes);
            foreach (Segment segment in segments)
            {
                _mapper.Map(ticket.Passenger, segment);
            }
            await _repository.AddSegmentsAsync(segments);

            return Ok();
        }

        [Route("refund")]
        [HttpPost]
        public async Task<ActionResult> RefundTicket(RefundDTO refund)
        {
            if (!await _validator.ValidateDTOAsync(refund))
            {
                return BadRequest();
            }

            if (!await _repository.RefundTicketAsync(refund.TicketNumber))
            {
                return Conflict();
            }

            return Ok();
        }
    }
}
