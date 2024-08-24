using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options): base(options)
        {
                
        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed Data for Difficulties - Easy, Medium, Hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("dc9da68d-9bfc-429a-b35d-674b61c4ca47"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("4b0ac70b-6f04-4d67-a079-b8177786e4b0"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("ab8fcab7-54e6-4a80-b9a8-8fc1ed4e1d1e"),
                    Name = "Hard"
                }
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //Seed Data for Regions
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.Parse("446ea138-b99b-4fff-8317-2b342d7f8bc8"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://images.pexels.com/photos/5342974/pexels-photo-5342974.jpeg"
                },
                new Region
                {
                    Id = Guid.Parse("6798d67e-fb34-40b5-abe7-cb8d8528aada"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("8a26a4c2-5649-4f6c-b5a3-25b30708ae64"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("591f72b4-4cdb-48aa-ae39-f97a9d039d82"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = "https://images.pexels.com/photos/10116189/pexels-photo-10116189.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2"
                },
                new Region
                {
                    Id = Guid.Parse("72e220ba-b323-4201-8715-674b7fa87c81"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/20788199/pexels-photo-20788199/free-photo-of-place-jacques-cartier-with-nelsons-column-in-montreal.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2"
                },
                new Region
                {
                    Id = Guid.Parse("f8e28077-244a-4122-a26e-9344a3fbdcd6"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                }
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    } 
}
