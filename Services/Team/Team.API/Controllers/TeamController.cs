using MediatR;
using Microsoft.AspNetCore.Mvc;
using Team.Application.Dtos;
using Team.Application.Features.Team.Commands.CreateTeam;
using Team.Application.Features.Team.Commands.DeleteTeam;
using Team.Application.Features.Team.Commands.UpdateTeam;
using Team.Application.Features.Team.Queries.GetTeamDetails;
using Team.Application.Features.Team.Queries.GetTeams;

namespace Team.API.Controllers
{
    public class TeamController : BaseAPIController
    {
        private readonly IMediator _mediator;

        public TeamController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDto>> Get(Guid id)
        {
            var teamDetails = await _mediator.Send(new GetTeamDetailsQuery(id));
            return Ok(teamDetails);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<TeamDto>>> Get()
        {
            var teams = await _mediator.Send(new GetTeamsQuery());
            return Ok(teams);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateTeamCommand team)
        {
            var result = await _mediator.Send(team);
            return CreatedAtAction(nameof(Get), result);
        }

        [HttpPut]
        public async Task<ActionResult> Put(UpdateTeamCommand team)
        {
            var result = await _mediator.Send(team);
            return CreatedAtAction(nameof(Get), result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteTeamCommand(id);
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(Get), result);
        }
    }
}
