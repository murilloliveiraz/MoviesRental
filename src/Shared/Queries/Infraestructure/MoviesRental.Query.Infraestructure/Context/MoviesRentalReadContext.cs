using MongoDB.Driver;
using MoviesRental.Query.Domain.Models;
using MoviesRental.Query.Infraestructure.Settings;

namespace MoviesRental.Query.Infraestructure.Context
{
    public class MoviesRentalReadContext : IMoviesRentalReadContext
    {
        public MoviesRentalReadContext(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Directors = database.GetCollection<Director>(settings.DirectorsCollection);
            Dvds = database.GetCollection<Dvd>(settings.DvdsCollection);
        }
        public IMongoCollection<Dvd> Dvds { get; }
        public IMongoCollection<Director> Directors { get; }
    }
}
