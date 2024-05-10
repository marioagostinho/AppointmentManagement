using AutoMapper;
using Team.Application.Dtos;
using Team.Application.Features.OpeningHours.Commands.CreateOpeningHours;
using Team.Application.Features.OpeningHours.Commands.UpdateOpeningHours;
using Team.Domain.Entities;

namespace Team.Application.MappingProfiles
{
    public class OpeningHoursProfile : Profile
    {
        public OpeningHoursProfile()
        {
            CreateMap<OpeningHours, OpeningHoursDto>()
                .ConstructUsing((oh, context) => new OpeningHoursDto(
                    oh.Id,
                    oh.DayOfWeek,
                    oh.OpeningHoursTimeSlots != null
                        ? context.Mapper.Map<IReadOnlyList<OpeningTimeSlotDto>>(oh.OpeningHoursTimeSlots.Select(ohts => ohts.OpeningTimeSlot))
                        : new List<OpeningTimeSlotDto>().AsReadOnly()
                ))
                .ReverseMap();

            CreateMap<CreateOpeningHoursCommand, OpeningHours>()
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek));

            CreateMap<UpdateOpeningHoursCommand, OpeningHours>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek));
        }
    }
}
