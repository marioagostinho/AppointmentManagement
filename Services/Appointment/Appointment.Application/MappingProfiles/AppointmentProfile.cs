using Entities = Appointment.Domain.Entities;
using Appointment.Application.Dtos;
using AutoMapper;
using Appointment.Application.Features.Appointment.Commands.CreateAppointment;
using Appointment.Application.Features.Appointment.Commands.UpdateAppointment;

namespace Appointment.Application.MappingProfiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Entities.Appointment, AppointmentDto>()
                .ReverseMap();

            CreateMap<CreateAppointmentCommand, Entities.Appointment>();
            CreateMap<UpdateAppointmentCommand, Entities.Appointment>();
        }
    }
}
