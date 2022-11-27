using System.Linq.Expressions;
using UserManager.Shared;
using UserManager.Shared.Enum;

namespace UserManager.Server.Service;

public interface IService<TE, TD> where TE : class, new() where TD : class, new()
{
    
    Task<TD?> GetById(int id, Website website);

    Task<List<TD>> GetByExpression(Expression<Func<TE, bool>> expression, Website website);

    Task<bool> Update(TD t, Website website);

    Task<bool> DeleteById(int id, Website website);

    Task<bool> Insert(TD t, Website website);

}