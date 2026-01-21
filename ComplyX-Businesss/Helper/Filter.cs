using ComplyX_Businesss.Models;
using ComplyX_Businesss.Helper;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Nest;
using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using X.PagedList;
using ComplyX.Shared.Helper;
using ComplyX.Shared.Data;

namespace ComplyX_Businesss.Helper
{

    public static class Filter
    {
        public class FilterDescriptor
        {
            public string Field { get; set; } = string.Empty;  // e.g., "FirstName"
            public string Operator { get; set; } = "equals";   // equals, contains, startswith, endswith, gt, lt, gte, lte
            public string Value { get; set; } = string.Empty;  // e.g., "John"
        }

        public static async Task<PageListed<T>> ToPagedListAsync<T>(
          this IQueryable<T> queryable,
          PagedListCriteria criteria,
          IDictionary<string, string>? orderByTranslations = null)
        {
            // 1️⃣ Total count before filters
            var totalCount = await queryable.CountAsync();
            var filterList = new List<FilterDescriptor>();
            var orderList = new List<SortDescriptor>();
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            // Deserialize filters
            if (criteria.Filters != null && criteria.Filters.Count > 0)
            {
                foreach (var f in criteria.Filters)
                {
                    if (!string.IsNullOrWhiteSpace(f))
                    {
                        try
                        {
                            var filterObj = JsonSerializer.Deserialize<FilterDescriptor>(f, jsonOptions);
                            if (filterObj != null) filterList.Add(filterObj);
                        }
                        catch
                        {
                            // Optional: log bad JSON
                        }
                    }
                }
            }

            // Deserialize order by
            if (criteria.OrderBy != null && criteria.OrderBy.Count > 0)
            {
                foreach (var o in criteria.OrderBy)
                {
                    if (!string.IsNullOrWhiteSpace(o))
                    {
                        try
                        {
                            var orderObj = JsonSerializer.Deserialize<SortDescriptor>(o, jsonOptions);
                            if (orderObj != null) orderList.Add(orderObj);
                        }
                        catch
                        {
                            // Optional: log bad JSON
                        }
                    }
                }
            }

            // 2️⃣ Apply filters
            if (filterList != null && filterList.Any())
            {
                var parameter = Expression.Parameter(typeof(T), "x"); // x =>
                Expression? combined = null;

                foreach (var filter in filterList)
                {
                    // Get the actual property name
                    var propertyName = orderByTranslations != null && orderByTranslations.ContainsKey(filter.Field)
                        ? orderByTranslations[filter.Field]
                        : filter.Field;

                    // Get the property expression: x.PropertyName
                    var property = Expression.PropertyOrField(parameter, propertyName);

                    // Convert the filter value to the right type
                    var targetType = property.Type;
                    var convertedValue = ConvertToType(filter.Value, targetType);
                    //var convertedValue = Convert.ChangeType(filter.Value, property.Type);
                    var constant = Expression.Constant(convertedValue, property.Type);
                    // Normalize property and constant for date-only comparisons
                    Expression left = property;
                    Expression right = constant;

                    // Special handling for DateTime/DateTime?
                    Expression? comparison;
                    if (property.Type == typeof(DateTime) || property.Type == typeof(DateTime?))
                    {
                        var date = ((DateTime)ConvertToType(filter.Value, typeof(DateTime))).Date;
                        var nextDate = date.AddDays(1);

                        var constDate = Expression.Constant(date, property.Type);
                        var constNextDate = Expression.Constant(nextDate, property.Type);

                        comparison = filter.Operator.ToLower() switch
                        {
                            "equals" => Expression.AndAlso(
                                            Expression.GreaterThanOrEqual(property, constDate),
                                            Expression.LessThan(property, constNextDate)),

                            "gt" => Expression.GreaterThan(property, constNextDate),
                            "lt" => Expression.LessThan(property, constDate),
                            "gte" => Expression.GreaterThanOrEqual(property, constDate),
                            "lte" => Expression.LessThanOrEqual(property, constNextDate),

                            _ => throw new NotSupportedException($"Operator '{filter.Operator}' is not supported for DateTime.")
                        };
                    }
                    else
                    {
                        comparison = filter.Operator.ToLower() switch
                        {
                            "equals" => Expression.Equal(left, right),
                            "gt" => Expression.GreaterThan(left, right),
                            "lt" => Expression.LessThan(left, right),
                            "gte" => Expression.GreaterThanOrEqual(left, right),
                            "lte" => Expression.LessThanOrEqual(left, right),

                            "contains" when property.Type == typeof(string) =>
                                Expression.Call(property, typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) })!, constant),

                            "startswith" when property.Type == typeof(string) =>
                                Expression.Call(property, typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) })!, constant),

                            "endswith" when property.Type == typeof(string) =>
                                Expression.Call(property, typeof(string).GetMethod(nameof(string.EndsWith), new[] { typeof(string) })!, constant),

                            _ => throw new NotSupportedException(
                                $"Operator '{filter.Operator}' is not supported for property type '{property.Type}'.")
                        };
                    }
                    // Combine expressions with AND
                    combined = combined == null ? comparison : Expression.AndAlso(combined, comparison);
                }

                if (combined != null)
                {
                    var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);
                    queryable = queryable.Where(lambda);
                }
            }


            // 3️⃣ Count after filtering
            var filteredCount = await queryable.CountAsync();

            // 4️⃣ Apply ordering
            if (orderList != null && orderList.Any())
            {
                bool firstOrder = true;

                foreach (var order in orderList)
                {
                    var propertyName = orderByTranslations != null && orderByTranslations.ContainsKey(order.Field)
                        ? orderByTranslations[order.Field]
                        : order.Field;

                    var parameter = Expression.Parameter(typeof(T), "x");
                    var property = Expression.PropertyOrField(parameter, propertyName);
                    var lambda = Expression.Lambda(property, parameter);

                    string methodName = firstOrder
                        ? (order.Direction.Equals("desc", StringComparison.OrdinalIgnoreCase) ? "OrderByDescending" : "OrderBy")
                        : (order.Direction.Equals("desc", StringComparison.OrdinalIgnoreCase) ? "ThenByDescending" : "ThenBy");

                    var method = typeof(Queryable).GetMethods()
                        .First(m => m.Name == methodName
                                    && m.GetParameters().Length == 2)
                        .MakeGenericMethod(typeof(T), property.Type);

                    queryable = (IQueryable<T>)method.Invoke(null, new object[] { queryable, lambda })!;
                    firstOrder = false;
                }
            }


            // 5️⃣ Apply paging
            if (criteria.Take > 0)
            {
                queryable = queryable.Skip(criteria.Skip).Take(criteria.Take);
            }
            // 6️⃣ Fetch items
            var items = await queryable.ToListAsync();
            PageListed<T> pageListed = new(items, totalCount, filteredCount);
            return pageListed;
        }
        public static PagedList<T> ToPagedList<T>(
 this IEnumerable<T> source,
 PagedListCriteria criteria,
 IDictionary<string, string>? orderByTranslations = null)
        {
            var totalCount = source.Count();
            var filterList = new List<FilterDescriptor>();
            var orderList = new List<SortDescriptor>();
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Deserialize filters
            if (criteria.Filters != null && criteria.Filters.Count > 0)
            {
                foreach (var f in criteria.Filters)
                {
                    if (!string.IsNullOrWhiteSpace(f))
                    {
                        try
                        {
                            var filterObj = JsonSerializer.Deserialize<FilterDescriptor>(f, jsonOptions);
                            if (filterObj != null) filterList.Add(filterObj);
                        }
                        catch
                        {
                            // Optional: log bad JSON
                        }
                    }
                }
            }

            // Deserialize order by
            if (criteria.OrderBy != null && criteria.OrderBy.Count > 0)
            {
                foreach (var o in criteria.OrderBy)
                {
                    if (!string.IsNullOrWhiteSpace(o))
                    {
                        try
                        {
                            var orderObj = JsonSerializer.Deserialize<SortDescriptor>(o, jsonOptions);
                            if (orderObj != null) orderList.Add(orderObj);
                        }
                        catch
                        {
                            // Optional: log bad JSON
                        }
                    }
                }
            }

            // Apply filters
            if (filterList.Any())
            {
                var parameter = Expression.Parameter(typeof(T), "x"); // x =>
                Expression? combined = null;

                foreach (var filter in filterList)
                {
                    var propertyName = orderByTranslations != null && orderByTranslations.ContainsKey(filter.Field)
                        ? orderByTranslations[filter.Field]
                        : filter.Field;

                    var property = Expression.PropertyOrField(parameter, propertyName);
                    var targetType = property.Type;

                    object? convertedValue = ConvertToType(filter.Value, targetType);
                    var constant = Expression.Constant(convertedValue, targetType);

                    Expression? comparison;

                    // Handle DateTime filtering
                    if (property.Type == typeof(DateTime) || property.Type == typeof(DateTime?))
                    {
                        var date = ((DateTime)ConvertToType(filter.Value, typeof(DateTime))).Date;
                        var nextDate = date.AddDays(1);

                        var constDate = Expression.Constant(date, property.Type);
                        var constNextDate = Expression.Constant(nextDate, property.Type);

                        comparison = filter.Operator.ToLower() switch
                        {
                            "equals" => Expression.AndAlso(
                                Expression.GreaterThanOrEqual(property, constDate),
                                Expression.LessThan(property, constNextDate)
                            ),
                            "gt" => Expression.GreaterThan(property, constNextDate),
                            "lt" => Expression.LessThan(property, constDate),
                            "gte" => Expression.GreaterThanOrEqual(property, constDate),
                            "lte" => Expression.LessThanOrEqual(property, constNextDate),
                            _ => throw new NotSupportedException($"Operator '{filter.Operator}' is not supported for DateTime.")
                        };
                    }
                    else if (property.Type == typeof(string))
                    {
                        var toLower = typeof(string).GetMethod(nameof(string.ToLower), Type.EmptyTypes)!;
                        var notNull = Expression.NotEqual(property, Expression.Constant(null, typeof(string)));
                        var propertyToLower = Expression.Call(property, toLower);
                        var constantToLower = Expression.Call(Expression.Constant(filter.Value.ToString()), toLower);

                        comparison = filter.Operator.ToLower() switch
                        {
                            "equals" => Expression.AndAlso(notNull, Expression.Equal(propertyToLower, constantToLower)),
                            "contains" => Expression.AndAlso(notNull, Expression.Call(propertyToLower, nameof(string.Contains), null, constantToLower)),
                            "startswith" => Expression.AndAlso(notNull, Expression.Call(propertyToLower, nameof(string.StartsWith), null, constantToLower)),
                            "endswith" => Expression.AndAlso(notNull, Expression.Call(propertyToLower, nameof(string.EndsWith), null, constantToLower)),
                            _ => throw new NotSupportedException($"Operator '{filter.Operator}' is not supported for string.")
                        };
                    }
                    else
                    {
                        comparison = filter.Operator.ToLower() switch
                        {
                            "equals" => Expression.Equal(property, constant),
                            "gt" => Expression.GreaterThan(property, constant),
                            "lt" => Expression.LessThan(property, constant),
                            "gte" => Expression.GreaterThanOrEqual(property, constant),
                            "lte" => Expression.LessThanOrEqual(property, constant),
                            _ => throw new NotSupportedException($"Operator '{filter.Operator}' is not supported for property type '{property.Type}'.")
                        };
                    }

                    combined = combined == null ? comparison : Expression.AndAlso(combined, comparison);
                }

                if (combined != null)
                {
                    var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter).Compile();
                    source = source.Where(lambda);
                }
            }

            var filteredCount = source.Count();

            // Apply ordering
            if (orderList != null && orderList.Any())
            {
                bool firstOrder = true;

                foreach (var order in orderList)
                {
                    var propertyName = orderByTranslations != null && orderByTranslations.ContainsKey(order.Field)
                        ? orderByTranslations[order.Field]
                        : order.Field;

                    var parameter = Expression.Parameter(typeof(T), "x");
                    var property = Expression.PropertyOrField(parameter, propertyName);
                    var propertyType = property.Type;
                    var lambdaType = typeof(Func<,>).MakeGenericType(typeof(T), propertyType);
                    var lambda = Expression.Lambda(lambdaType, property, parameter).Compile();

                    if (firstOrder)
                    {
                        source = order.Direction.Equals("desc", StringComparison.OrdinalIgnoreCase)
                            ? EnumerableOrderByDescending(source, lambda, propertyType)
                            : EnumerableOrderBy(source, lambda, propertyType);
                        firstOrder = false;
                    }
                    else
                    {
                        source = order.Direction.Equals("desc", StringComparison.OrdinalIgnoreCase)
                            ? EnumerableThenByDescending((IOrderedEnumerable<T>)source, lambda, propertyType)
                            : EnumerableThenBy((IOrderedEnumerable<T>)source, lambda, propertyType);
                    }
                }
            }

            // Apply paging
            if (criteria.Take > 0)
            {
                source = source.Skip(criteria.Skip).Take(criteria.Take);
            }

            return new PagedList<T>(source.ToList(), totalCount, filteredCount);
        }
    
        public static object? ConvertToType(object value, Type targetType)
        {
            if (value == null || value is DBNull)
                return null;

            // Handle Nullable<T>
            var underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

            // Empty string handling
            if (value is string str && string.IsNullOrWhiteSpace(str))
            {
                return targetType.IsValueType && Nullable.GetUnderlyingType(targetType) == null
                    ? Activator.CreateInstance(underlyingType) // non-nullable → default
                    : null;                                    // nullable → null
            }

            try
            {
                // Use TypeConverter if possible
                var converter = TypeDescriptor.GetConverter(underlyingType);
                if (converter != null && converter.CanConvertFrom(value.GetType()))
                {
                    return converter.ConvertFrom(value);
                }

                // Try Convert.ChangeType
                return Convert.ChangeType(value, underlyingType);
            }
            catch
            {
                // Fallback for enums
                if (underlyingType.IsEnum && value is string enumStr)
                {
                    return Enum.Parse(underlyingType, enumStr, ignoreCase: true);
                }

                throw new InvalidCastException($"Cannot convert value '{value}' to type {targetType}.");
            }
        }

        private static IEnumerable<T> EnumerableOrderByDescending<T>(IEnumerable<T> source, Delegate keySelector, Type keyType)
        {
            var method = typeof(Enumerable)
                .GetMethods()
                .First(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), keyType);

            return (IEnumerable<T>)method.Invoke(null, new object[] { source, keySelector })!;
        }

        private static IEnumerable<T> EnumerableThenBy<T>(IOrderedEnumerable<T> source, Delegate keySelector, Type keyType)
        {
            var method = typeof(Enumerable)
                .GetMethods()
                .First(m => m.Name == "ThenBy" && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), keyType);

            return (IOrderedEnumerable<T>)method.Invoke(null, new object[] { source, keySelector })!;
        }

        private static IEnumerable<T> EnumerableThenByDescending<T>(IOrderedEnumerable<T> source, Delegate keySelector, Type keyType)
        {
            var method = typeof(Enumerable)
                .GetMethods()
                .First(m => m.Name == "ThenByDescending" && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), keyType);

            return (IOrderedEnumerable<T>)method.Invoke(null, new object[] { source, keySelector })!;
        }
        private static IEnumerable<T> EnumerableOrderBy<T>(IEnumerable<T> source, Delegate keySelector, Type keyType)
        {
            var method = typeof(Enumerable)
                .GetMethods()
                .First(m => m.Name == "OrderBy" && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), keyType);

            return (IEnumerable<T>)method.Invoke(null, new object[] { source, keySelector })!;
        }

    }
}
