using AutoMapper;
using tickets.DTO;
using tickets.Model;

namespace tickets.Profiles
{
    public class RefundProfile : Profile
    {
        public RefundProfile()
        {
            CreateMap<RefundDTO, Refund>().AfterMap((_, d) => {
                d.OperationTimeTimezone = (short)-d.OperationTime.Offset.Hours;
            });
        }
    }
}
