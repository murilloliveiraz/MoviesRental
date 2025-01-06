using MediatR;
using MoviesRental.Query.Application.Contracts;

namespace MoviesRental.Query.Application.Features.Dvds.Commands.DeleteDvd
{
    public class DeleteDvdCommandHandler : IRequestHandler<DeleteDvdCommand, bool>
    {
        private readonly IDvdQueryRepository _repository;

        public DeleteDvdCommandHandler(IDvdQueryRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteDvdCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id) || request.DeletedAt > DateTime.Now)
                return false;

            var dvd = await _repository.Get(request.Id);
            if (dvd is null)
                return false;

            dvd.DeletedAt = DateTime.Now;
            dvd.Available = false;
            dvd.Copies = 0;

            return await _repository.Update(dvd);
        }
    }
}
