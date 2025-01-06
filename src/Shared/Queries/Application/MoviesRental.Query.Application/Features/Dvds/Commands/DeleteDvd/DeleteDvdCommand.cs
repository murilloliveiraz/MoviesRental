using MediatR;

namespace MoviesRental.Query.Application.Features.Dvds.Commands.DeleteDvd
{
    public record DeleteDvdCommand(string Id, DateTime DeletedAt): IRequest<bool>;
}
