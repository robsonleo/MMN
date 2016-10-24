using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Controls;
using System.Windows.Data;

namespace CustomFunctions.Extensions
{
    //todo добавить Compare(object obj1, object obj2)
    public static class ExtensionOrderBy
    {
        public static IQueryable<T> ExpressionTreeOrderWithPriority<T>(this IQueryable<T> source, IEnumerable<string> columnsForSort, SortOrder sortOrder)
        {
            return columnsForSort.Aggregate(source, (current, sortColumn) => current.OrderColumns(sortColumn, sortOrder));
        }

        public static IQueryable<T> ExpressionTreeOrderWithPriority<T>(this IQueryable<T> source, IEnumerable<GridViewColumn> columnsForSort,
            SortOrder sortOrder)
        {
            return columnsForSort.Aggregate(source, (current, sortColumn) => current.OrderColumns(((Binding)sortColumn.DisplayMemberBinding).Path.Path, sortOrder));
        }

        public static IEnumerable<T> ExpressionTreeOrderWithPriority<T>(this IEnumerable<T> source, IEnumerable<string> columnsForSort, SortOrder sortOrder)
        {
            return ExpressionTreeOrderWithPriority(source.AsQueryable(), columnsForSort, sortOrder);
        }

        public static IEnumerable<T> ExpressionTreeOrderWithPriority<T>(this IEnumerable<T> source, IEnumerable<GridViewColumn> columnsForSort,
            SortOrder sortOrder)
        {
            return ExpressionTreeOrderWithPriority(source.AsQueryable(), columnsForSort, sortOrder);
        }

        public static IQueryable<T> OrderColumns<T>(this IQueryable<T> source, string sortProperty,
            SortOrder sortOrder)
        {
            if (source.Any())
            {
                Expression<Func<Object, string>> orderByExp = (p) => p.GetType().GetProperty(sortProperty).GetValue(p, null).ToString();
                var typeArguments = new Type[] { typeof(object), typeof(string) };
                var methodName = sortOrder == SortOrder.Ascending ? "OrderBy" : "OrderByDescending";
                var resultExp = Expression.Call(typeof(Queryable), methodName, typeArguments, source.Expression,
                    Expression.Quote(orderByExp));
                return source.Provider.CreateQuery<T>(resultExp);
            }
            return source;
        }


    }
}
