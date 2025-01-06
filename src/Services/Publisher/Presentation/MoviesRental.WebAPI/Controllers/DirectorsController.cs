using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesRental.Core;
using MoviesRental.Query.Application.Features.Directors.Queries.GetDirector;
using System.Net;

namespace MoviesRental.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DirectorsController(IMediator mediator)
        {
            _mediator = mediator;
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
    }
}
