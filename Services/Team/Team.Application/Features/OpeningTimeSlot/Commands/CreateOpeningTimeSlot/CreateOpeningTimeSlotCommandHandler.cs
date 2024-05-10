using AutoMapper;
using MediatR;
using Team.Domain.Repositories;
using Entities = Team.Domain.Entities;

namespace Team.Application.Features.OpeningTimeSlot.Commands.CreateOpeningTimeSlot
{
    public class CreateOpeningTimeSlotCommandHandler : IRequestHandler<CreateOpeningTimeSlotCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IOpeningTimeSlotRepository _openingTimeSlotRepository;

        public CreateOpeningTimeSlotCommandHandler(IMapper mapper, IOpeningTimeSlotRepository openingTimeSlotRepository)
        {
            _mapper = mapper;
            _openingTimeSlotRepository = openingTimeSlotRepository;
        }

        public async Task<Guid> Handle(CreateOpeningTimeSlotCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateOpeningTimeSlotCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (validatorResult.Errors.Any())
                throw new Exception();

            var openingTimeSlot = _mapper.Map<Entities.OpeningTimeSlot>(request);
            await _openingTimeSlotRepository.CreateAsync(openingTimeSlot);

            return openingTimeSlot.Id;
        }
    }
}
