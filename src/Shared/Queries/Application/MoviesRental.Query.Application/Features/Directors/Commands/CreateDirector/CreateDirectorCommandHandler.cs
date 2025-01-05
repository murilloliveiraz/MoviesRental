using MediatR;
using MoviesRental.Query.Application.Contracts;
using MoviesRental.Query.Domain.Models;

namespace MoviesRental.Query.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorCommandHandler: IRequestHandler<CreateDirectorCommand,bool>
    {
        private readonly IDirectorQueryRepository _repository;
        private readonly CreateDirectorCommandValidator _validator;

        public CreateDirectorCommandHandler(IDirectorQueryRepository repository, CreateDirectorCommandValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<bool> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
        {
            var validationRules = _validator.Validate(request);
            
            if (!validationRules.IsValid)
                return false;
            
            var director = await _repository.Get(request.Id);
            
            if(director is not null)
                return false;

            director = new Director { Id = request.Id, FullName = request.FullName, CreatedAt = request.CreatedAt, UpdatedAt = request.UpdatedAt };

            var result = await _repository.Create(director);
            
            if (result is null)
                return false;

            return true;
        }
    }
}
