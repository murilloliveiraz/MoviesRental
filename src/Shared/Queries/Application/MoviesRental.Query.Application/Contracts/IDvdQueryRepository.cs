using MoviesRental.Query.Domain.Models;

namespace MoviesRental.Query.Application.Contracts
{
    public interface IDvdQueryRepository : IQueryRepository<Dvd>
    {
        Task<Dvd> GetByTitle(string title);
    }
}
