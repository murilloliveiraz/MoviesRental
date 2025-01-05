using MediatR;

namespace MoviesRental.Query.Application.Features.Directors.Commands.DeleteDirector
{
    public record DeleteDirectorCommand(string Id): IRequest<bool>;
}
