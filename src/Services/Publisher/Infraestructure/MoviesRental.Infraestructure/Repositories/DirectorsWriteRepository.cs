using Microsoft.EntityFrameworkCore;
using MoviesRental.Application.Contracts;
using MoviesRental.Domain.Entities;
using MoviesRental.Infraestructure.Context;

namespace MoviesRental.Infraestructure.Repositories
{
    public class DirectorsWriteRepository : IDirectorsWriteRepository
    {
        private readonly MoviesRentalWriteContext _context;

        public DirectorsWriteRepository(MoviesRentalWriteContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Director entity)
        {
            await _context.Directors.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid Id) =>
            await _context.Directors.Where(x => x.Id == Id).ExecuteDeleteAsync() > 0;
 

        public async Task<Director> Get(Guid Id)
        {
            return await _context.Directors.FindAsync(Id);
        }

        public async Task<Director> GetDirectorWithMovies(Guid Id)
        {
            return await _context.Directors.AsNoTracking()
                                            .Include(x => x.Dvds)
                                            .Where(x => x.Id == Id)
                                            .FirstOrDefaultAsync();
        }

        public async Task<bool> Update(Director entity)
        {
            _context.Directors.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
