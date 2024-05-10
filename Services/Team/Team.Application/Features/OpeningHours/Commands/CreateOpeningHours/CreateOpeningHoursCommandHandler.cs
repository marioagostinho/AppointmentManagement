using AutoMapper;
using MediatR;
using Team.Domain.Entities;
using Team.Domain.Repositories;
using Entities = Team.Domain.Entities;

namespace Team.Application.Features.OpeningHours.Commands.CreateOpeningHours
{
    public class CreateOpeningHoursCommandHandler : IRequestHandler<CreateOpeningHoursCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IOpeningHoursRepository _openingHoursRepository;
        private readonly IOpeningTimeSlotRepository _openingTimeSlotRepository;

        public CreateOpeningHoursCommandHandler(IMapper mapper, IOpeningHoursRepository openingHoursRepository, IOpeningTimeSlotRepository openingTimeSlotRepository)
        {
            _mapper = mapper;
            _openingHoursRepository = openingHoursRepository;
            _openingTimeSlotRepository = openingTimeSlotRepository;
        }

        public async Task<Guid> Handle(CreateOpeningHoursCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateOpeningHoursCommandValidator(_openingTimeSlotRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (validatorResult.Errors.Any())
                throw new Exception();

            var openingHours = _mapper.Map<Entities.OpeningHours>(request);

            // Instantiate OpeningHoursTimeSlots Relationship
            openingHours.OpeningHoursTimeSlots = request.OpeningTimeSlotIds.Select(id => new OpeningHoursTimeSlot
            {
                OpeningHoursId = openingHours.Id,
                OpeningTimeSlotId = id
            }).ToList();

            await _openingHoursRepository.CreateAsync(openingHours);

            return openingHours.Id;
        }
    }
}
