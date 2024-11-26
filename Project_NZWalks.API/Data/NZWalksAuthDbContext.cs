using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_NZWalks.API.Models.User;

namespace Project_NZWalks.API.Data;

public class NzWalksAuthDbContext(DbContextOptions<NzWalksAuthDbContext> options) 
    : IdentityDbContext<AppUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        const string readerRoleId = "8d367432-820c-44ab-b5e4-33eed0868846";
        const string writerRoleId = "bdeb44bb-e5b4-4de9-adfd-d51420b4235a";

        var roles = new List<IdentityRole>
        {
            new()
            {
                Id = readerRoleId,
                ConcurrencyStamp = readerRoleId,
                Name = "Reader",
                NormalizedName = "Reader".ToUpper()
            },
            new()
            {
                Id=writerRoleId,
                ConcurrencyStamp=writerRoleId,
                Name = "Writer",
                NormalizedName = "Writer".ToUpper()
            }
        };

        builder.Entity<IdentityRole>().HasData(roles);
    }
}
