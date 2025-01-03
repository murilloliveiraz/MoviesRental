using MediatR;

namespace MoviesRental.Application.Features.Directors.Commands.DeleteDirector
{
    public record DeleteDirectorCommand(Guid Id): IRequest<bool>;
}
