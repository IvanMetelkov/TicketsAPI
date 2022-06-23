using AutoMapper;
using tickets.DTO;
using tickets.Model;

namespace tickets.Profiles
{
    public class SegmentsProfile : Profile
    {
        public SegmentsProfile()
        {
            CreateMap<PassengerDTO, Segment>();
            CreateMap<SegmentDTO, Segment>().AfterMap((_, d) =>
            {
                d.ArriveTimeTimezone = (short)d.ArriveDatetime.Offset.Hours;
                d.DepartTimeTimezone = (short)d.DepartDatetime.Offset.Hours;
            });
        }
    }
}
