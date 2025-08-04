using MediatR;
using Microsoft.AspNetCore.Mvc;
using TrainManagement.DTOs;
using TrainManagement.Helpers;
using TrainManagement.MediatR.Commands;
using TrainManagement.MediatR.Queries;
using TrainManagement.Params;

namespace TrainManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrainManagementController(
        IMediator _mediator
        ) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedList<ComponentIdDTO>>> GetItems([FromQuery]ComponentParams componentParams, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetComponentsQuery(componentParams), cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet]
        [Route("get/{id:int}")]
        public async Task<ActionResult<ComponentIdDTO>> GetItem(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetComponentQuery(id), cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet]
        [Route("get/{uniquenumber}")]
        public async Task<ActionResult<ComponentIdDTO>> GetItem(string uniquenumber, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetComponentQuery(uniquenumber), cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<ActionResult> CreateItem([FromBody] ComponentDTO dto, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateComponentCommand(dto), cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Created();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateItem([FromBody] ComponentIdDTO dto, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateComponentCommand(dto), cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteComponentCommand(id), cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpDelete]
        [Route("{uniqueNumber}")]
        public async Task<ActionResult> Delete(string uniqueNumber, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteComponentCommand(uniqueNumber), cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok();
        }
    }
}
