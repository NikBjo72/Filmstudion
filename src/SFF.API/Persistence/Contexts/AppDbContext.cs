using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SFF.API.Domain.Entities;

namespace SFF.API.Persistence.Contexts
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<FilmCopy> FilmCopies { get; set; }
        public DbSet<FilmStudio> FilmStudios { get; set; }
        //public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
           
            protected override void OnModelCreating(ModelBuilder builder)
            {

            base.OnModelCreating(builder);

            builder.Entity<Film>().ToTable("Films");
            builder.Entity<Film>().HasKey(p => p.FilmId);
            builder.Entity<Film>().Property(p => p.FilmId).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Film>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Film>().Property(p => p.Country).IsRequired().HasMaxLength(30);
            builder.Entity<Film>().Property(p => p.Director).IsRequired().HasMaxLength(30);
            //builder.Entity<Film>().HasMany(p => p.FilmCopies).WithOne(p => p.Film).HasForeignKey(p => p.FilmId);

            builder.Entity<FilmStudio>().ToTable("FilmStudios");
            builder.Entity<FilmStudio>().HasKey(p => p.FilmStudioId);
            builder.Entity<FilmStudio>().Property(p => p.FilmStudioId).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<FilmStudio>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<FilmStudio>().Property(p => p.City).IsRequired().HasMaxLength(30);


            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.Role).IsRequired();
            builder.Entity<User>().Property(p => p.UserName).IsRequired().HasMaxLength(30);
            builder.Entity<User>().Property(p => p.Password).IsRequired();
            builder.Entity<User>().Property(p => p.Token);

            builder.Entity<FilmCopy>().ToTable("FilmCopies");
            builder.Entity<FilmCopy>().Property(p => p.FilmCopyId).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<FilmCopy>().Property(p => p.FilmId).IsRequired();
            builder.Entity<FilmCopy>().Property(p => p.RentedOut).IsRequired();


            builder.Entity<Film>().HasData
            (
                new Film {
                    FilmId = "1",
                    Name = "Die Hard",
                    ReleaseDate = new DateTime(1988,07,15),
                    Country = "USA",
                    Director = "John McTiernan",
                    NumberOfCopies = 3,
                    AvailableForRent = true,
                    MaxRentDays = 20
                    },
                new Film {
                    FilmId = "2",
                    Name = "Die Hard 2",
                    ReleaseDate = new DateTime(1990,07,4),
                    Country = "USA",
                    Director = "Renny Harlin",
                    NumberOfCopies = 4,
                    AvailableForRent = true,
                    MaxRentDays = 15
                    },
                new Film {
                    FilmId = "3",
                    Name = "Die Hard - Hämningslöst",
                    ReleaseDate = new DateTime(1995,05,19),
                    Country = "USA",
                    Director = "John McTiernan",
                    NumberOfCopies = 6,
                    AvailableForRent = true,
                    MaxRentDays = 10
                    },
                new Film {
                    FilmId = "4",
                    Name = "Die Hard 4.0",
                    ReleaseDate = new DateTime(2007,06,27),
                    Country = "USA",
                    Director = "Len Wiseman",
                    NumberOfCopies = 6,
                    AvailableForRent = true,
                    MaxRentDays = 12
                    }      
            );
            builder.Entity<FilmCopy>().HasData
            (
                new FilmCopy {
                    FilmCopyId = "101",
                    FilmId = "1",
                    RentedOut = false,
                    Rented = DateTime.Today,
                    },
                    new FilmCopy {
                    FilmCopyId = "102",
                    FilmId = "1",
                    RentedOut = false,
                    Rented = DateTime.Today,
                    },
                    new FilmCopy {
                    FilmCopyId = "103",
                    FilmId = "1",
                    RentedOut = false,
                    Rented = DateTime.Today,
                    }

            );
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "admin",
                NormalizedName = "ADMIN" 
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "filmstudio",
                NormalizedName = "FILMSTUDIO"
            });

        }
    }
}