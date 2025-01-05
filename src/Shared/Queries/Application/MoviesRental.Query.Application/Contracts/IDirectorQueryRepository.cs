using MoviesRental.Query.Domain.Models;

namespace MoviesRental.Query.Application.Contracts
{
    public interface IDirectorQueryRepository: IQueryRepository<Director>
    {
        Task<Director> GetByName(string name);
    }
}
