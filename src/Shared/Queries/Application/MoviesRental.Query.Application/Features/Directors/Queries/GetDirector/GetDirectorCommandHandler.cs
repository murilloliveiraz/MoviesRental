using MediatR;
using MoviesRental.Query.Application.Contracts;

namespace MoviesRental.Query.Application.Features.Directors.Queries.GetDirector
{
    public class GetDirectorCommandHandler : IRequestHandler<GetDirectorQuery, GetDirectorResponse>
    {
        private readonly IDirectorQueryRepository _repository;

        public GetDirectorCommandHandler(IDirectorQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetDirectorResponse> Handle(GetDirectorQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.FullName))
                return default;

            var director = await _repository.GetByName(request.FullName);
            
            if(director is null)
                return default;

            return new GetDirectorResponse(director.Id, director.FullName);
        }
    }
}
