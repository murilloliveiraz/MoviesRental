using MoviesRental.Query.Application.Contracts;
using MoviesRental.Query.Domain.Models;
using MoviesRental.Query.Infraestructure.Context;
using MongoDB.Driver;

namespace MoviesRental.Query.Infraestructure.Repositories
{
    public class DvdsQueryRepository : IDvdQueryRepository
    {
        private readonly IMoviesRentalReadContext _context;

        public DvdsQueryRepository(IMoviesRentalReadContext context)
        {
            _context = context;
        }

        public async Task<Dvd> Create(Dvd entity)
        {
            await _context.Dvds.InsertOneAsync(entity);
            return entity;
        }

        public async Task<bool> Delete(string Id)
        {
            var result = await _context.Dvds.DeleteOneAsync(d => d.Id == Id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<Dvd> Get(string Id)
        {
            return await _context.Dvds.Find(d => d.Id == Id && d.Available).FirstOrDefaultAsync();
        }

        public async Task<Dvd> GetByTitle(string title)
        {
            return await _context.Dvds.Find(d => d.Title == title).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Dvd entity)
        {
            var result = await _context.Dvds.ReplaceOneAsync(d => d.Id == entity.Id, entity);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}
