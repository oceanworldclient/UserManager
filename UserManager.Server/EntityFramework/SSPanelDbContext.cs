using Microsoft.EntityFrameworkCore;
using UserManager.Server.Model;

namespace UserManager.Server.EntityFramework;

public class SSPanelDbContext: DbContext
{

    public SSPanelDbContext(DbContextOptions<SSPanelDbContext> options) : base(options)
    {
    }
}