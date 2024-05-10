using AutoMapper;
using Team.Application.Dtos;
using Team.Application.Features.OpeningTimeSlot.Commands.CreateOpeningTimeSlot;
using Team.Application.Features.OpeningTimeSlot.Commands.UpdateOpeningTimeSlot;
using Team.Domain.Entities;

namespace Team.Application.MappingProfiles
{
    public class OpeningTimeSlotProfile : Profile
    {
        public OpeningTimeSlotProfile()
        {
            CreateMap<OpeningTimeSlotDto, OpeningTimeSlot>().ReverseMap();
            CreateMap<CreateOpeningTimeSlotCommand, OpeningTimeSlot>();
            CreateMap<UpdateOpeningTimeSlotCommand, OpeningTimeSlot>();
        }
    }
}
