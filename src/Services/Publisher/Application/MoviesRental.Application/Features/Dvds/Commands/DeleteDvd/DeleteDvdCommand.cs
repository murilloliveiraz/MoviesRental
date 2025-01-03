using MediatR;

namespace MoviesRental.Application.Features.Dvds.Commands.DeleteDvd
{
    public record DeleteDvdCommand(Guid Id) : IRequest<DeleteDvdResponse>;
}
