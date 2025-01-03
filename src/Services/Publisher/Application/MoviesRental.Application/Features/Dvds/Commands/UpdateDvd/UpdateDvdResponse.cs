namespace MoviesRental.Application.Features.Dvds.Commands.UpdateDvd
{
    public record UpdateDvdResponse(
        string Id,
        string Title,
        string Genre,
        DateTime Published,
        string DirectorId,
        int Copies,
        DateTime UpdatedAt
        );
}
