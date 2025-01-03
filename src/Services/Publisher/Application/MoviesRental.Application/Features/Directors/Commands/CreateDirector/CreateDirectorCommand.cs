using MediatR;

namespace MoviesRental.Application.Features.Directors.Commands.CreateDirector
{
    public record CreateDirectorCommand(string Name, string Surname): IRequest<CreateDirectorResponse>;
}
