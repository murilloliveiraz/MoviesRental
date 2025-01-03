using MediatR;
using MoviesRental.Application.Contracts;
using MoviesRental.Domain.Entities;

namespace MoviesRental.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorCommandHandler : IRequestHandler<CreateDirectorCommand, CreateDirectorResponse>
    {
        private readonly IDirectorsWriteRepository _repository;
        private readonly CreateDirectorCommandValidator _validator;

        public CreateDirectorCommandHandler(IDirectorsWriteRepository repository, CreateDirectorCommandValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<CreateDirectorResponse> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
        {
            var validationRules = _validator.Validate(request);
            if (!validationRules.IsValid)
                return default;
            var director = new Director(request.Name, request.Surname);
            var result = await _repository.Create(director);
            if (!result)
                return default;
            return new CreateDirectorResponse(director.Id.ToString(), director.FullName(), director.CreatedAt, director.UpdatedAt);

        }
    }
}
