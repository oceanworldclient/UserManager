using Microsoft.EntityFrameworkCore;
using UserManager.Server.Model;

namespace UserManager.Server.EntityFramework;

public class SSPanelDbContext: DbContext
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<Bought> Boughts { get; set; }
    
    public DbSet<Shop> Shops { get; set; }

    public SSPanelDbContext(DbContextOptions<SSPanelDbContext> options) : base(options)
    {
    }
    
}