using Entities = Appointment.Domain.Entities;
using Appointment.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Appointment.Application.Features.Appointment.Commands.UpdateAppointment
{
    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;

        public UpdateAppointmentCommandHandler(IMapper mapper, IAppointmentRepository appointmentRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<bool> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateAppointmentCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (validatorResult.Errors.Any())
                throw new Exception();

            var appointment = _mapper.Map<Entities.Appointment>(request);
            var result = await _appointmentRepository.UpdateAsync(appointment);

            return result;
        }
    }
}
