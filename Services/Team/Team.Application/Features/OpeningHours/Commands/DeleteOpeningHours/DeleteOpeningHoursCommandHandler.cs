using AutoMapper;
using MediatR;
using Team.Domain.Repositories;

namespace Team.Application.Features.OpeningHours.Commands.DeleteOpeningHours
{
    public class DeleteOpeningHoursCommandHandler : IRequestHandler<DeleteOpeningHoursCommand, bool>
    {
        private readonly IOpeningHoursRepository _openingHoursRepository;

        public DeleteOpeningHoursCommandHandler(IMapper mapper, IOpeningHoursRepository openingHoursRepository)
        {
            _openingHoursRepository = openingHoursRepository;
        }

        public async Task<bool> Handle(DeleteOpeningHoursCommand request, CancellationToken cancellationToken)
        {
            var openingHours = await _openingHoursRepository.GetByIdAsync(request.Id);

            if (openingHours == null)
                throw new Exception();

            bool result = await _openingHoursRepository.DeleteAsync(openingHours);

            return result;
        }
    }
}
