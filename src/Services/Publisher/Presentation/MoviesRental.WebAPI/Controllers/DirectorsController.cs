﻿using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoviesRental.Application.Features.Directors.Commands.CreateDirector;
using MoviesRental.Application.Features.Directors.Commands.DeleteDirector;
using MoviesRental.Application.Features.Directors.Commands.UpdateDirector;
using MoviesRental.Core;
using MoviesRental.Core.EventBus.Events;
using MoviesRental.Query.Application.Features.Directors.Queries.GetDirector;
using System.Net;

namespace MoviesRental.WebAPI.Controllers
{
    public class DirectorsController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;

        public DirectorsController(IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet("[action]/{fullName}", Name = "GetDirector")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetDirector([FromRoute] string fullName)
        {
            var query = new GetDirectorQuery(fullName);
            var response = await _mediator.Send(query, HttpContext.RequestAborted);

            if(response is null)
                return CustomResponse((int)HttpStatusCode.NotFound, false);

            return CustomResponse((int)HttpStatusCode.OK, true, response);
        }

        [HttpPost("CreateDirector")]
        [ProducesResponseType(typeof(CreateDirectorResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CreateDirectorResponse>> CreateDirector([FromBody] CreateDirectorCommand command)
        {
            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (response is null)
                return CustomResponse((int)HttpStatusCode.BadRequest, false);

            var @event = new DirectorCreatedEvent(response.Id, response.FullName, response.CreatedAt, response.UpdatedAt);

            await _publishEndpoint.Publish(@event);

            return CustomResponse((int)HttpStatusCode.Created, true, response);
        }

        [HttpPut("UpdateDirector")]
        [ProducesResponseType(typeof(UpdateDirectorResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<UpdateDirectorResponse>> UpdateDirector([FromBody] UpdateDirectorCommand command)
        {
            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (response is null)
                return CustomResponse((int)HttpStatusCode.BadRequest, false);

            var @event = new DirectorUpdatedEvent(response.Id, response.FullName, response.UpdatedAt);

            await _publishEndpoint.Publish(@event);

            return CustomResponse((int)HttpStatusCode.OK, true, response);
        }

        [HttpDelete("DeleteDirector/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> DeleteDirector([FromRoute] Guid id)
        {
            var command = new DeleteDirectorCommand(id);
            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (!response)
                return CustomResponse((int)HttpStatusCode.BadRequest, false);

            var @event = new DirectorDeletedEvent(id.ToString());

            await _publishEndpoint.Publish(@event);

            return CustomResponse((int)HttpStatusCode.OK, response);
        }
    }
}
