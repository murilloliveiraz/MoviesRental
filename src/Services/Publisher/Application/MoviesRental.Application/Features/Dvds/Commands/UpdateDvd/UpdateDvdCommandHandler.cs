using FluentValidation;
using MediatR;
using MoviesRental.Application.Contracts;

namespace MoviesRental.Application.Features.Dvds.Commands.UpdateDvd
{
    public class UpdateDvdCommandHandler : IRequestHandler<UpdateDvdCommand, UpdateDvdResponse>
    {
        private readonly IDvdWriteRepository _repository;
        private readonly UpdateDvdCommandValidator _validator;

        public UpdateDvdCommandHandler(IDvdWriteRepository repository, UpdateDvdCommandValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<UpdateDvdResponse> Handle(UpdateDvdCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return default;

            var dvd = await _repository.Get(request.Id);

            dvd.UpdateTitle(request.Title);
            dvd.UpdateGenre(request.Genre);
            dvd.UpdateDirector(request.DirectorId);
            dvd.UpdateCopies(request.Copies);
            dvd.UpdatePublishedDate(request.Published);

            var result = await _repository.Update(dvd);

            if (!result)
                return default;

            return new UpdateDvdResponse(
                dvd.Id.ToString(),
                dvd.Title,
                dvd.Genre.ToString(),
                dvd.Published,
                dvd.DirectorId.ToString(),
                dvd.Copies,
                dvd.UpdatedAt
            );
        }
    }
}
