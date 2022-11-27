using System.Linq.Expressions;

namespace UserManager.Shared.Utils;

public static class TransExp<TIn, TOut>
{
    private static readonly Func<TIn, TOut> Cache = GetFunc();
    
    private static Func<TIn, TOut> GetFunc()
    {
        var parameterExpression = Expression.Parameter(typeof(TIn), "p");

        var memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), (from item in typeof(TOut).GetProperties() where item.CanWrite let property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name)) select Expression.Bind(item, property)).Cast<MemberBinding>().ToArray());
        var lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, parameterExpression);

        return lambda.Compile();
    }

    public static TOut Trans(TIn tIn)
    {
        return Cache(tIn);
    }
}