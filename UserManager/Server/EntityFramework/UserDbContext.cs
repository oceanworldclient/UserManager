using Microsoft.EntityFrameworkCore;

namespace UserManager.Server.EntityFramework;

public class UserDbContext: DbContext
{
    
    
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
        
    }
}