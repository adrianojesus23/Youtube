using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static DummyJsonApi.WebApi.DelegateMethod;

public static class VehicleExtensions
{
    // Método de extensão para filtrar um único item
    public static TSource Filter<TSource>(this TSource vehicle, Func<TSource, bool> predicate) where TSource : class
    {
        if (vehicle == null) throw new ArgumentNullException(nameof(vehicle));
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));

        return predicate(vehicle) ? vehicle : null;
    }

    // Método de extensão para filtrar uma coleção de itens
    public static IEnumerable<TSource> Where1<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));

        return source.Where(predicate);
    }

 public static Func<VehicleModel, string, bool> func = (vehicle, filter) => vehicle.description.Contains(filter);
       public static IEnumerable<TSource> Where3<TSource>(this IEnumerable<TSource> source, Func<TSource, string, bool> predicate, string filter)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));

        return source.Where(x=> predicate(x,filter));
    }

    // Método de extensão para filtrar uma consulta LINQ
    public static IQueryable<TSource> Where2<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));

        return source.Where(predicate);
    }
}
