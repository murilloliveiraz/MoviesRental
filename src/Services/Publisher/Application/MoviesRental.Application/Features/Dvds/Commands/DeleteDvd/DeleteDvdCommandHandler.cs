using MediatR;
using MoviesRental.Application.Contracts;
using MoviesRental.Application.Features.Directors.Commands.DeleteDirector;

namespace MoviesRental.Application.Features.Dvds.Commands.DeleteDvd
{
    public class DeleteDvdCommandHandler : IRequestHandler<DeleteDvdCommand, DeleteDvdResponse>
    {
        private readonly IDvdWriteRepository _repository;

        public DeleteDvdCommandHandler(IDvdWriteRepository repository)
        {
            _repository = repository;
        }

        public async Task<DeleteDvdResponse> Handle(DeleteDvdCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
                return default;
            
            var dvd = await _repository.Get(request.Id);
            
            if (dvd is null)
                return default;

            dvd.Delete();

            var result = await _repository.Update(dvd);

            if (!result)
                return default;

            return new DeleteDvdResponse(dvd.Id.ToString(), (DateTime)dvd.DeletedAt);
        }
    }
}
