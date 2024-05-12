using MediatR;
using Microsoft.AspNetCore.Mvc;
using Team.Application.Dtos;
using Team.Application.Features.OpeningHours.Commands.CreateOpeningHours;
using Team.Application.Features.OpeningHours.Commands.DeleteOpeningHours;
using Team.Application.Features.OpeningHours.Commands.UpdateOpeningHours;
using Team.Application.Features.OpeningHours.Queries.GetOpeningHours;
using Team.Application.Features.OpeningHours.Queries.GetOpeningHoursByTeamDate;
using Team.Application.Features.OpeningHours.Queries.GetOpeningHoursDetails;

namespace Team.API.Controllers
{
    public class OpeningHoursController : BaseAPIController
    {
        private readonly IMediator _mediator;

        public OpeningHoursController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OpeningHoursDto>> Get(Guid id)
        {
            var openingHourDetail = await _mediator.Send(new GetOpeningHoursDetailsQuery(id));
            return Ok(openingHourDetail);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OpeningHoursDto>>> Get()
        {
            var openingHours = await _mediator.Send(new GetOpeningHoursQuery());
            return Ok(openingHours);
        }

        [HttpGet("{teamId}/{date}")]
        public async Task<ActionResult<IReadOnlyList<OpeningHoursDto>>> Get(Guid teamId, DateTime date)
        {
            var availableOpeningHours = await _mediator.Send(new GetOpeningHoursByTeamDateQuery(teamId, date));
            return Ok(availableOpeningHours);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateOpeningHoursCommand openingHours)
        {
            var result = await _mediator.Send(openingHours);
            return CreatedAtAction(nameof(Get), result);
        }

        [HttpPut]
        public async Task<ActionResult> Put(UpdateOpeningHoursCommand openingHours)
        {
            var result = await _mediator.Send(openingHours);
            return CreatedAtAction(nameof(Get), result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteOpeningHoursCommand(id);
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(Get), result);
        }
    }
}
