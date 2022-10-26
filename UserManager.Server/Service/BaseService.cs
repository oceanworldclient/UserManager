using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserManager.Server.EntityFramework;
using UserManager.Shared;

namespace UserManager.Server.Service;

public class BaseService<TE, TD> : IService<TE, TD> where TE : class, new() where TD : class, new()
{
    protected SSPanelDbContext? DbContext { get; private set; }

    protected IMapper Mapper { get; }

    public BaseService(IMapper mapper)
    {
        Mapper = mapper;
    }

    protected void InitialDbContext(Website website)
    {
        if (DbContext != null) return;
        var option = new DbContextOptionsBuilder<SSPanelDbContext>()
            .UseMySQL(ServiceConfig.Instance.GetConnectionString(website.ToString()))
            .Options;
        DbContext = new SSPanelDbContext(option);
    }

    public async Task<TD?> GetById(int id, Website website)
    {
        InitialDbContext(website);
        var entity = await DbContext!.Set<TE>().FindAsync(id);
        return entity == null ? new TD() : Mapper.Map<TD>(entity);
    }

    public async Task<List<TD>> GetByExpression(Expression<Func<TE, bool>> expression, Website website)
    {
        InitialDbContext(website);
        var list = await DbContext!.Set<TE>().Where(expression).ToListAsync();
        return Mapper.Map<List<TD>>(list);
    }

    public async Task<bool> Update(TD t, Website website)
    {
        try
        {
            var entity = Mapper.Map<TE>(t);
            return await Update(entity, website);
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> Update(TE entity, Website website)
    {
        try
        {
            InitialDbContext(website);
            var set = DbContext!.Set<TE>();
            set.Attach(entity);
            foreach (var p in entity.GetType().GetProperties())
            {
                if (p.GetValue(entity) != null)
                {
                    DbContext.Entry(entity).Property(p.Name).IsModified = true;
                }
            }

            await DbContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public Task<bool> DeleteById(int id, Website website)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Insert(TD t, Website website)
    {
        try
        {
            InitialDbContext(website);
            var entity = Mapper.Map<TE>(t);
            await DbContext!.Set<TE>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}