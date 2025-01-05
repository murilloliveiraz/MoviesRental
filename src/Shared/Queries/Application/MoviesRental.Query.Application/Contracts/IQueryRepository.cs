﻿namespace MoviesRental.Query.Application.Contracts
{
    public interface IQueryRepository<T> where T : class
    {
        Task<T> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(string Id);
        Task<T> Get(string Id);
    }
}
