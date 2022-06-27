using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Schema;
using System.Collections.Generic;
using System.Threading.Tasks;
using tickets.DTO;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using FluentValidation;

namespace tickets.Validation
{
    public class Validator : IValidator
    {
        private readonly IMemoryCache _cache;
        private readonly IValidator<PassengerDTO> _passengerValidator;
        private readonly IValidator<SegmentDTO> _segmentValidator;
        private readonly IValidator<RefundDTO> _refundValidator;
        private readonly IValidator<TicketDTO> _ticketValidator;

        public Validator(IMemoryCache cache, IValidator<PassengerDTO> passengerValidator, IValidator<SegmentDTO> segmentValidator, IValidator<RefundDTO> refundValidator, 
            IValidator<TicketDTO> ticketValidator)
        {
            _cache = cache;
            _passengerValidator = passengerValidator;
            _segmentValidator = segmentValidator;
            _refundValidator = refundValidator;
            _ticketValidator = ticketValidator;
        }

        public async Task<bool> ValidateDTOAsync(IRequestDTO requestDTO)
        {
            AsyncLazy<JSchema> schema = null;
            if (requestDTO.OperationType != null)
            {
                _cache.TryGetValue(requestDTO.OperationType, out schema);
            }

            if (schema != null)
            {
                DefaultContractResolver contractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };

                JObject request = JObject.FromObject(requestDTO, new JsonSerializer
                {
                    Formatting = Formatting.Indented,
                    ContractResolver = contractResolver
                });

                return request.IsValid(await schema);
            }

            return false;
        }

        public bool ValidatePassenger(PassengerDTO passenger)
        {
            return _passengerValidator.Validate(passenger).IsValid;
        }

        public bool ValidateRefund(RefundDTO refund)
        {
            return _refundValidator.Validate(refund).IsValid;
        }

        public bool ValidateSale(TicketDTO ticket)
        {
            return _ticketValidator.Validate(ticket).IsValid;
        }

        public bool ValidateSegments(IEnumerable<SegmentDTO> segments)
        {
            foreach (SegmentDTO segment in segments)
            {
                if (!_segmentValidator.Validate(segment).IsValid)
                {
                    return false;
                }
            }

            return true;
        }
    }
}