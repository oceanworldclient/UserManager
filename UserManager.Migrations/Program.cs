// See https://aka.ms/new-console-template for more information

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManager.Server.EntityFramework;

var options = new DbContextOptionsBuilder<UserDbContext>()
    .UseMySQL("server=localhost;port=3306;database=identity_user;user=root;password=orz.10089")
    .Options;
using var ssPanelContext = new UserDbContext(options);

ssPanelContext.Database.EnsureDeleted();
ssPanelContext.Database.EnsureCreated();

List<IdentityRole> roles = new()
{
    new IdentityRole()
    {
        Id = "1",
        Name = "God",
        NormalizedName = "GOD",
        ConcurrencyStamp = Guid.NewGuid().ToString()
    },
    new IdentityRole()
    {
        Id = "2",
        Name = "User",
        NormalizedName = "USER",
        ConcurrencyStamp = Guid.NewGuid().ToString()
    }
};

ssPanelContext.Roles.AddRange(roles);


ssPanelContext.SaveChanges();