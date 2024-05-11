using Appointment.Application.Dtos;
using Appointment.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Appointment.Application.Features.Appointment.Queries.GetAppointments
{
    public class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsQuery, IReadOnlyList<AppointmentDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAppointmentsQueryHandler(IMapper mapper, IAppointmentRepository appointmentRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IReadOnlyList<AppointmentDto>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetAllAsync();
            var result = _mapper.Map<IReadOnlyList<AppointmentDto>>(appointments);

            return result;
        }
    }
}
