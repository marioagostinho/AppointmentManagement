using MediatR;
using Microsoft.AspNetCore.Mvc;
using Team.Application.Dtos;
using Team.Application.Features.OpeningTimeSlot.Commands.CreateOpeningTimeSlot;
using Team.Application.Features.OpeningTimeSlot.Commands.DeleteOpeningTimeSlot;
using Team.Application.Features.OpeningTimeSlot.Commands.UpdateOpeningTimeSlot;
using Team.Application.Features.OpeningTimeSlot.Queries.GetOpeningTimeSlotDetails;
using Team.Application.Features.OpeningTimeSlot.Queries.GetOpeningTimeSlots;
using Team.Application.Features.OpeningTimeSlot.Queries.GetOpeningTimeSlotsByTeamAndDate;

namespace Team.API.Controllers
{
    public class OpeningTimeSlotController : BaseAPIController
    {
        private readonly IMediator _mediator;

        public OpeningTimeSlotController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OpeningTimeSlotDto>> Get(Guid id)
        {
            var openingTimeSlot = await _mediator.Send(new GetOpeningTimeSlotDetailsQuery(id));
            return Ok(openingTimeSlot);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OpeningTimeSlotDto>>> Get()
        {
            var openingTimeSlot = await _mediator.Send(new GetOpeningTimeSlotsQuery());
            return Ok(openingTimeSlot);
        }

        [HttpGet("{teamId}/{date}")]
        public async Task<ActionResult<IReadOnlyList<OpeningHoursDto>>> Get(Guid teamId, DateTime date)
        {
            var openingHours = await _mediator.Send(new GetOpeningTimeSlotsByTeamAndDateQuery(teamId, date));
            return Ok(openingHours);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateOpeningTimeSlotCommand openingTimeSlot)
        {
            var result = await _mediator.Send(openingTimeSlot);
            return CreatedAtAction(nameof(Get), result);
        }

        [HttpPut]
        public async Task<ActionResult> Put(UpdateOpeningTimeSlotCommand openingTimeSlot)
        {
            var result = await _mediator.Send(openingTimeSlot);
            return CreatedAtAction(nameof(Get), result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteOpeningTimeSlotCommand(id);
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(Get), result);
        }
    }
}
