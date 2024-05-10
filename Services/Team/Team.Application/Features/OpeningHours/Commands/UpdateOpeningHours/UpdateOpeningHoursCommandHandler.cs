using AutoMapper;
using MediatR;
using Team.Domain.Entities;
using Team.Domain.Repositories;
using Entities = Team.Domain.Entities;

namespace Team.Application.Features.OpeningHours.Commands.UpdateOpeningHours
{
    public class UpdateOpeningHoursCommandHandler : IRequestHandler<UpdateOpeningHoursCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IOpeningHoursRepository _openingHoursRepository;
        private readonly IOpeningTimeSlotRepository _openingTimeSlotRepository;

        public UpdateOpeningHoursCommandHandler(IMapper mapper, IOpeningHoursRepository openingHoursRepository, IOpeningTimeSlotRepository openingTimeSlotRepository)
        {
            _mapper = mapper;
            _openingHoursRepository = openingHoursRepository;   
            _openingTimeSlotRepository = openingTimeSlotRepository;
        }

        public async Task<bool> Handle(UpdateOpeningHoursCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateOpeningHoursCommandValidator(_openingHoursRepository, _openingTimeSlotRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (validatorResult.Errors.Any())
                throw new Exception();

            var openingHours = _mapper.Map<Entities.OpeningHours>(request);

            // Instance OpeningHoursTimeSlots Relationship
            openingHours.OpeningHoursTimeSlots = request.OpeningTimeSlotIds.Select(id => new OpeningHoursTimeSlot
            {
                OpeningHoursId = openingHours.Id,
                OpeningTimeSlotId = id
            }).ToList();

            bool result = await _openingHoursRepository.UpdateAsync(openingHours);

            return result;
        }
    }
}
