using MediatR;

namespace MoviesRental.Application.Features.Dvds.Commands.ReturnDvd
{
    public record ReturnDvdCommand(Guid Id): IRequest<ReturnDvdResponse>;
}
