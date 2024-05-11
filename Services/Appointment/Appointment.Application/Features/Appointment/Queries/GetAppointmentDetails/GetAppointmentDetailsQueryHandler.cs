using Appointment.Application.Dtos;
using Appointment.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Appointment.Application.Features.Appointment.Queries.GetAppointmentDetails
{
    public class GetAppointmentDetailsQueryHandler : IRequestHandler<GetAppointmentDetailsQuery, AppointmentDto>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAppointmentDetailsQueryHandler(IMapper mapper, IAppointmentRepository appointmentRepository)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<AppointmentDto> Handle(GetAppointmentDetailsQuery request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(request.Id);
            var result =  _mapper.Map<AppointmentDto>(appointment);

            return result;
        }
    }
}
