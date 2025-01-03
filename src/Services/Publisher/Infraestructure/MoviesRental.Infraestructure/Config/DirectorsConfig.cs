﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesRental.Domain.Entities;

namespace MoviesRental.Infraestructure.Config
{
    public class DirectorsConfig : IEntityTypeConfiguration<Director>
    {
        public void Configure(EntityTypeBuilder<Director> builder)
        {
            builder.ToTable("Directors");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(Director.MAX_LENGTH);

            builder.Property(x => x.Surname)
                .IsRequired()
                .HasMaxLength(Director.MAX_LENGTH);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired();

            builder.HasMany(x => x.Dvds)
                .WithOne(d => d.Director);
        }
    }
}
