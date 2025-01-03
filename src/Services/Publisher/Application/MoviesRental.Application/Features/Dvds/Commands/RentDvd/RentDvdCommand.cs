using MediatR;

namespace MoviesRental.Application.Features.Dvds.Commands.RentDvd
{
    public record RentDvdCommand(Guid Id): IRequest<RentDvdResponse>;
}
