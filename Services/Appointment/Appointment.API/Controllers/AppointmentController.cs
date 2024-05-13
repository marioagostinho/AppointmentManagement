using Appointment.Application.Dtos;
using Appointment.Application.Features.Appointment.Commands.CreateAppointment;
using Appointment.Application.Features.Appointment.Commands.DeleteAppointment;
using Appointment.Application.Features.Appointment.Commands.UpdateAppointment;
using Appointment.Application.Features.Appointment.Queries.GetAppointmentDetails;
using Appointment.Application.Features.Appointment.Queries.GetAppointments;
using Appointment.Application.Features.Appointment.Queries.GetAppointmentsByTeamAndDate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.API.Controllers
{
    public class AppointmentController : BaseAPIController
    {
        private readonly IMediator _mediator;

        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> Get(Guid id)
        {
            var appointment = await _mediator.Send(new GetAppointmentDetailsQuery(id));
            return Ok(appointment);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> Get()
        {
            var appointments = await _mediator.Send(new GetAppointmentsQuery());
            return Ok(appointments);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateAppointmentCommand appointment)
        {
            var result = await _mediator.Send(appointment);
            return CreatedAtAction(nameof(Get), result);
        }

        [HttpPut]
        public async Task<ActionResult> Put(UpdateAppointmentCommand appointment)
        {
            var result = await _mediator.Send(appointment);
            return CreatedAtAction(nameof(Get), result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteAppointmentCommand(id);
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(Get), result);
        }
    }
}
