using Microsoft.EntityFrameworkCore;
using MoviesRental.Application.Contracts;
using MoviesRental.Domain.Entities;
using MoviesRental.Infraestructure.Context;

namespace MoviesRental.Infraestructure.Repositories
{
    public class DvdsWriteRepository : IDvdWriteRepository
    {
        private readonly MoviesRentalWriteContext _context;

        public DvdsWriteRepository(MoviesRentalWriteContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Dvd entity)
        {
            await _context.Dvds.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await _context.Dvds.Where(x => x.Id == Id).ExecuteDeleteAsync();
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Dvd> Get(Guid Id)
        {
            return await _context.Dvds.FindAsync(Id);
        }

        public async Task<bool> Update(Dvd entity)
        {
            _context.Dvds.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
