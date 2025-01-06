using MoviesRental.Query.Application.Contracts;
using MoviesRental.Query.Domain.Models;
using MoviesRental.Query.Infraestructure.Context;
using MongoDB.Driver;

namespace MoviesRental.Query.Infraestructure.Repositories
{
    public class DirectorsQueryRepository : IDirectorQueryRepository
    {
        private readonly IMoviesRentalReadContext _context;

        public DirectorsQueryRepository(IMoviesRentalReadContext context)
        {
            _context = context;
        }

        public async Task<Director> Create(Director entity)
        {
            await _context.Directors.InsertOneAsync(entity);
            return entity;
        }

        public async Task<bool> Delete(string Id)
        {
            var result = await _context.Directors.DeleteOneAsync(d => d.Id == Id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<Director> Get(string Id)
        {
            return await _context.Directors.Find(d => d.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<Director> GetByName(string name)
        {
            return await _context.Directors.Find(d => d.FullName == name).FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Director entity)
        {
            var result = await _context.Directors.ReplaceOneAsync(d => d.Id == entity.Id, entity);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}
