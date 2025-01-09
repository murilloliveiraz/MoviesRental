using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesRental.Application.Features.Dvds.Commands.CreateDvd;
using MoviesRental.Application.Features.Dvds.Commands.DeleteDvd;
using MoviesRental.Application.Features.Dvds.Commands.RentDvd;
using MoviesRental.Application.Features.Dvds.Commands.ReturnDvd;
using MoviesRental.Application.Features.Dvds.Commands.UpdateDvd;
using MoviesRental.Core;
using MoviesRental.Core.EventBus.Events;
using MoviesRental.Query.Application.Features.Dvds.Queries.GetDvd;
using MoviesRental.WebAPI.Cache;
using System.Net;

namespace MoviesRental.WebAPI.Controllers
{
    public class DvdsController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly ICacheRepository _cacheRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public DvdsController(IMediator mediator, ICacheRepository cacheRepository, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator;
            _cacheRepository = cacheRepository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet("[action]/{title}", Name = "GetDvd")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetDvd([FromRoute] string title)
        {
            var response = await _cacheRepository.Get(title);

            if(response is not null)
                return CustomResponse((int)HttpStatusCode.OK, true, response);

            var query = new GetDvdQuery(title);

            response = await _mediator.Send(query, HttpContext.RequestAborted);

            if (response is null)
                return CustomResponse((int)HttpStatusCode.NotFound, false);

            await _cacheRepository.Update(response);

            return CustomResponse((int)HttpStatusCode.OK, true, response);
        }

        [HttpPost("CreateDvd")]
        [ProducesResponseType(typeof(CreateDvdResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CreateDvdResponse>> CreateDvd([FromBody] CreateDvdCommand command)
        {
            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (response is null)
                return CustomResponse((int)HttpStatusCode.BadRequest, false);

            var @event = new DvdCreatedEvent(
                response.Id,
                response.Title,
                response.Genre,
                response.Published,
                response.Available,
                response.Copies,
                response.DirectorId,
                response.CreatedAt,
                response.UpdatedAt
                );

            await _publishEndpoint.Publish(@event);

            return CustomResponse((int)HttpStatusCode.Created, true, response);
        }

        [HttpPut("UpdateDvd")]
        [ProducesResponseType(typeof(UpdateDvdResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<UpdateDvdResponse>> UpdateDvd([FromBody] UpdateDvdCommand command)
        {
            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (response is null)
                return CustomResponse((int)HttpStatusCode.BadRequest, false);

            var @event = new DvdUpdatedEvent(
                response.Id,
                response.Title,
                response.Genre,
                response.Published,
                response.Copies,
                response.DirectorId,
                response.UpdatedAt
                );

            await _publishEndpoint.Publish(@event);

            return CustomResponse((int)HttpStatusCode.OK, true, response);
        }

        [HttpPut("RentDvd/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> RentDvd([FromRoute] Guid id)
        {
            var command = new RentDvdCommand(id);
            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (response is null)
                return CustomResponse((int)HttpStatusCode.BadRequest, false);

            var @event = new DvdRentedEvent(id.ToString(), response.UpdatedAt);

            await _publishEndpoint.Publish(@event);

            return CustomResponse((int)HttpStatusCode.OK, true, response);
        }

        [HttpPut("ReturnDvd/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> ReturnDvd([FromRoute] Guid id)
        {
            var command = new ReturnDvdCommand(id);
            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (response is null)
                return CustomResponse((int)HttpStatusCode.BadRequest, false);

            var @event = new DvdReturnedEvent(id.ToString(), response.UpdatedAt);

            await _publishEndpoint.Publish(@event);

            return CustomResponse((int)HttpStatusCode.OK, true, response);
        }

        [HttpDelete("DeleteDvd/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> DeleteDvd([FromRoute] Guid id)
        {
            var command = new DeleteDvdCommand(id);
            
            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if (response is null)
                return CustomResponse((int)HttpStatusCode.BadRequest, false);

            var @event = new DvdDeletedEvent(id.ToString(), response.DeletedAt);

            await _publishEndpoint.Publish(@event);

            return CustomResponse((int)HttpStatusCode.OK, true);
        }
    }
}
