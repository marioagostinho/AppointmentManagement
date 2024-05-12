using Appointment.Application.Dtos;
using Appointment.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Appointment.Application.Features.Appointment.Queries.GetAppointmentsByTeamAndDate
{
    public class GetAppointmentsByTeamAndDateQueryHandler : IRequestHandler<GetAppointmentsByTeamAndDateQuery, IReadOnlyList<AppointmentDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAppointmentsByTeamAndDateQueryHandler(IMapper mapper, IAppointmentRepository appointmentRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IReadOnlyList<AppointmentDto>> Handle(GetAppointmentsByTeamAndDateQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetByTeamIdAndDateAsync(request.TeamId, request.Date);
            var result = _mapper.Map<IReadOnlyList<AppointmentDto>>(appointments);

            return result;
        }
    }
}
