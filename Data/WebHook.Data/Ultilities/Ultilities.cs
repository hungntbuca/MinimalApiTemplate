using System;
using System.Linq;
using System.Linq.Expressions;

namespace WebHook.EntityFrameWork.Ultilities
{
    public static class Ultilities
    {
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
            => condition ? source.Where(predicate) : source;
    }
}
