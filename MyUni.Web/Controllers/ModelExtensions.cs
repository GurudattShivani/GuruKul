using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Compression;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Web.UI;
using Gurukul.Web.Infrastructure;

namespace Gurukul.Web.Controllers
{
    public static class BSDataTableExtensions
    {
        [Obsolete("This method is no longer required, since we use DLINQ now, and importantly this method will work when the collection is in memory, and will fail when it transforms to an SQL expression")]
        private static  IEnumerable<OrderExpression<T>> GetExpressionList<T>(IEnumerable<DataTableColumnInfo> orderedColumns) where T : class
        {
            if (orderedColumns == null)
            {
                return null;
            }

            var expressions = orderedColumns.ToList().Select(column =>
            {
                var propertyInfo = typeof(T).GetProperty(column.Field);
                if (propertyInfo == null)
                {
                    return null;
                }

                Func<T, object> func = arg => propertyInfo.GetValue(arg, null);

                return new OrderExpression<T>
                {
                    ColumnOrder = column.ColumnOrder,
                    Func = func
                };
            }).Where(x => x != null).ToList();

            return expressions;

        }

        public static IQueryable<T> GetOrderedCollection<T>(this IQueryable<T> collection, IEnumerable<DataTableColumnInfo> orderedColumns) where T : class
        {
            if (orderedColumns == null)
            {
                return collection;
            }
            //
            // Use DLINQ to create the order by expressions. In DLINQ we can pass multiple order by statements in a comma separated string
            //
            var orderExpression = string.Join(",", orderedColumns.Select(x => string.Format("{0} {1}", x.Field, x.ColumnOrder)));
            
            return collection.OrderBy(orderExpression);
        }

        public static IQueryable<object> ToDataSource<T>(this DataTableInfo dataTableInfo, IQueryable<T> collection, Expression<Func<T, bool>> filterExpression = null, Expression<Func<T,object>> projectionExpression = null) where T : class
        {
            if (collection == null)
            {
                return null;
            }
            //
            // Order By
            //
            var orderByExpression = dataTableInfo.OrderByExpression;
            var orderedCollection = string.IsNullOrEmpty(orderByExpression) ? collection : collection.OrderBy(orderByExpression).AsQueryable();
            //
            // Filter
            //
            var filteredCollection = filterExpression == null ? orderedCollection : orderedCollection.Where(filterExpression);
            //
            // Projection
            //
            var projectedCollection = projectionExpression == null ? filteredCollection : filteredCollection.Select(projectionExpression);
            //
            // Paging
            //
            var pagedCollection = string.IsNullOrEmpty(orderByExpression) ? projectedCollection.Take(dataTableInfo.Length) :
                projectedCollection.Skip(dataTableInfo.PageNumber*dataTableInfo.Length).Take(dataTableInfo.Length);
            

            return pagedCollection;

        }
    }
}