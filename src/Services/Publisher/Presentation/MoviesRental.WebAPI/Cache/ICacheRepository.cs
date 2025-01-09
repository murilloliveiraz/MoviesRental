using MoviesRental.Query.Application.Features.Dvds.Queries.GetDvd;

namespace MoviesRental.WebAPI.Cache
{
    public interface ICacheRepository
    {
        Task<GetDvdResponse> Get(string title);
        Task Update(GetDvdResponse response);
    }
}
