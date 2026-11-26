using AutoMapper;
using Queue_WebApi.CQRS.Appointments.Commands.CreateAppointments;
using Queue_WebApi.Models;

namespace Queue_WebApi.Mappings
{
    public class AppointmentMappingProfile : Profile
    {
        public AppointmentMappingProfile()
        {
            CreateMap<AddAppointmentCommand, Apointment>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
