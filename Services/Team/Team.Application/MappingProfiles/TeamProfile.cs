using AutoMapper;
using Team.Application.Dtos;
using Team.Application.Features.Team.Commands.CreateTeam;
using Team.Application.Features.Team.Commands.UpdateTeam;
using Entities = Team.Domain.Entities;

namespace Team.Application.MappingProfiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Entities.Team, TeamDto>()
                 .ConstructUsing((t, context) => new TeamDto(
                    t.Id,
                    t.Name,
                    t.AppointmentDuration,
                    t.AmountOfAppointments,
                    t.TeamOpeningHours != null
                        ? context.Mapper.Map<IReadOnlyList<OpeningHoursDto>>(t.TeamOpeningHours.Select(toh => toh.OpeningHours))
                        : new List<OpeningHoursDto>().AsReadOnly()
                 ))
                .ReverseMap();

            CreateMap<CreateTeamCommand, Entities.Team>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.AppointmentDuration, opt => opt.MapFrom(src => src.AppointmentDuration))
                .ForMember(dest => dest.AmountOfAppointments, opt => opt.MapFrom(src => src.AmountOfAppointments));

            CreateMap<UpdateTeamCommand, Entities.Team>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.AppointmentDuration, opt => opt.MapFrom(src => src.AppointmentDuration))
                .ForMember(dest => dest.AmountOfAppointments, opt => opt.MapFrom(src => src.AmountOfAppointments));
        }
    }
}
