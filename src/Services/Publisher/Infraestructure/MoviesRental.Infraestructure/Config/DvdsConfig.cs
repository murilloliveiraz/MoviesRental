﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoviesRental.Domain.Entities;

namespace MoviesRental.Infraestructure.Config
{
    public class DvdsConfig : IEntityTypeConfiguration<Dvd>
    {
        public void Configure(EntityTypeBuilder<Dvd> builder)
        {
            builder.ToTable("Dvds");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Title)
                .IsUnique();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(Dvd.MAX_TITLE_LENGTH);

            builder.Property(x => x.Copies)
                .IsRequired();

            builder.Property(x => x.Available)
                .IsRequired();

            builder.Property(x => x.Published)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired();

            builder.Property(x => x.DirectorId)
                .IsRequired();

            builder.HasOne(x => x.Director)
                .WithMany(d => d.Dvds)
                .HasForeignKey(x => x.DirectorId);

            builder.Property(x => x.Genre)
                .HasConversion(v => v.ToString(), v => (EGenre)Enum.Parse(typeof(EGenre), v));
        }
    }
}
