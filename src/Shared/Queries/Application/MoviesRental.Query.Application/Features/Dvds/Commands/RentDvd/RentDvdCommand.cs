using MediatR;

namespace MoviesRental.Query.Application.Features.Dvds.Commands.RentDvd
{
    public record RentDvdCommand(string Id, DateTime UpdatedAt) : IRequest<bool>;
}
