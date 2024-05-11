using Entities = Appointment.Domain.Entities;
using Appointment.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Appointment.Application.Features.Appointment.Commands.CreateAppointment
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;

        public CreateAppointmentCommandHandler(IMapper mapper, IAppointmentRepository appointmentRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Guid> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateAppointmentCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (validatorResult.Errors.Any())
                throw new Exception();

            var appointment = _mapper.Map<Entities.Appointment>(request);
            await _appointmentRepository.CreateAsync(appointment);

            return appointment.Id;
        }
    }
}
