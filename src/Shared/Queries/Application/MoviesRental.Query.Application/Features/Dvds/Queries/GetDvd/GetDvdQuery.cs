using MediatR;

namespace MoviesRental.Query.Application.Features.Dvds.Queries.GetDvd
{
    public record GetDvdQuery(string Title): IRequest<GetDvdResponse>;
}
