﻿using Microsoft.EntityFrameworkCore;
using MoviesRental.Domain.Entities;

namespace MoviesRental.Infraestructure.Context
{
    public class MoviesRentalWriteContext : DbContext
    {
        public MoviesRentalWriteContext(){}

        public MoviesRentalWriteContext(DbContextOptions<MoviesRentalWriteContext> options): base(options) { }

        public DbSet<Director> Directors { get; set; }
        public DbSet<Dvd> Dvds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoviesRentalWriteContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
