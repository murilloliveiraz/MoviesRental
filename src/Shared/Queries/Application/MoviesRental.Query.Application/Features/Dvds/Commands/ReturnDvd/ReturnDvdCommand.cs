using MediatR;

namespace MoviesRental.Query.Application.Features.Dvds.Commands.ReturnDvd
{
    public record ReturnDvdCommand(string Id, DateTime UpdatedAt): IRequest<bool>;
}
