using Microsoft.EntityFrameworkCore;
using Project_NZWalks.API.Models.Domain;

namespace Project_NZWalks.API.Data;

public class NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) 
    : DbContext(dbContextOptions)
{
    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }
    public DbSet<Image> Images { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data for Difficulties
        // Easy, Medium, Hard
        var difficulties = new List<Difficulty>()
        {
            new()
            {
                Id = Guid.Parse("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"),
                Name = "Easy"
            },
            new()
            {
                Id = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                Name = "Medium"
            },
            new()
            {
                Id = Guid.Parse("f808ddcd-b5e5-4d80-b732-1ca523e48434"),
                Name = "Hard"
            }
        };

        // Seed difficulties to the database
        modelBuilder.Entity<Difficulty>().HasData(difficulties);



        // Seed data for Regions
        var regions = new List<Region>
        {
            new() {
                Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                Name = "Auckland",
                Code = "AKL",
                UserId = "3c781f81-f753-45b6-9676-7a98629fb86f",
                RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            },
            new() {
                Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                Name = "Northland",
                Code = "NTL",
                UserId = "ae5899b0-6f9e-4e18-a804-a6045edc37d2",
                RegionImageUrl = null
            },
            new() {
                Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                Name = "Bay Of Plenty",
                Code = "BOP",
                UserId = "3c781f81-f753-45b6-9676-7a98629fb86f",
                RegionImageUrl = null
            },
            new() {
                Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                Name = "Wellington",
                Code = "WGN",
                UserId = "ae5899b0-6f9e-4e18-a804-a6045edc37d2",
                RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            },
            new() {
                Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                Name = "Nelson",
                Code = "NSN",
                UserId = "ae5899b0-6f9e-4e18-a804-a6045edc37d2",
                RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            },
            new() {
                Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                Name = "Southland",
                Code = "STL",
                UserId = "3c781f81-f753-45b6-9676-7a98629fb86f",
                RegionImageUrl = null
            },
        };

        modelBuilder.Entity<Region>().HasData(regions);
    }

}
