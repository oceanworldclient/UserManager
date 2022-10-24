using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserManager.Server.EntityFramework;

public class UserDbContext: IdentityDbContext
{

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityUser>(entity => { 
            entity.Property(m => m.Email).HasMaxLength(127); 
            entity.Property(m => m.NormalizedEmail).HasMaxLength(127); 
            entity.Property(m => m.NormalizedUserName).HasMaxLength(127); 
            entity.Property(m => m.UserName).HasMaxLength(127); 
        }); 
        builder.Entity<IdentityRole>(entity => { 
            entity.Property(m => m.Name).HasMaxLength(127); 
            entity.Property(m => m.NormalizedName).HasMaxLength(127); 
        }); 
        builder.Entity<IdentityUserLogin<string>>(entity => 
        { 
            entity.Property(m => m.LoginProvider).HasMaxLength(127); 
            entity.Property(m => m.ProviderKey).HasMaxLength(127); 
        }); 
        builder.Entity<IdentityUserRole<string>>(entity => 
        { 
            entity.Property(m => m.UserId).HasMaxLength(127); 
            entity.Property(m => m.RoleId).HasMaxLength(127); 
        }); 
        builder.Entity<IdentityUserToken<string>>(entity => 
        { 
            entity.Property(m => m.UserId).HasMaxLength(127); 
            entity.Property(m => m.LoginProvider).HasMaxLength(127); 
            entity.Property(m => m.Name).HasMaxLength(127);
        }); 
    }
    
}