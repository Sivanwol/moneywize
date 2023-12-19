using System.Linq.Expressions;
namespace Domain.DTO.Filters.Fields;

public class FloatFilterField : BaseFilterField<FilterNumberOperation, float> {

    public override IQueryable<T> ApplyFilter<T>(IQueryable<T> query) {
        Expression valuesExpression = null;
        Expression filterExpression = null;
        Expression valueExpression = null;
        // Accessing the property with provided field name
        var parameter = Expression.Parameter(typeof(T), "t");
        var property = Expression.Property(parameter, FillterField); // Creating filter expression based on operation and type of TFilterValue
        if (FilterOperation == FilterNumberOperation.Between ||
            FilterOperation == FilterNumberOperation.NotBetween ||
            FilterOperation == FilterNumberOperation.In ||
            FilterOperation == FilterNumberOperation.NotIn) {
            if (FilterValues == null) {
                throw new InvalidOperationException("Filter values are not provided.");
            }
            valuesExpression = Expression.Constant(FilterValues, typeof(ICollection<int>));
        } else {
            if (FilterValue == null) {
                throw new InvalidOperationException("Filter value is not provided.");
            }
            valueExpression = Expression.Constant(FilterValue, typeof(int));
        }
        switch (FilterOperation) {
            case FilterNumberOperation.In:
                filterExpression = Expression.Call(typeof(Enumerable), nameof(Enumerable.Contains), new[] {
                    typeof(int)
                }, valuesExpression!, property);
                break;
            case FilterNumberOperation.NotIn:
                filterExpression = Expression.Not(Expression.Call(typeof(Enumerable), nameof(Enumerable.Contains), new[] {
                    typeof(int)
                }, valuesExpression!, property));
                break;
            case FilterNumberOperation.Equal:
                filterExpression = Expression.Equal(property, valueExpression!);
                break;
            case FilterNumberOperation.NotEqual:
                filterExpression = Expression.NotEqual(property, valueExpression!);
                break;
            case FilterNumberOperation.GreaterThan:
                filterExpression = Expression.GreaterThan(property, valueExpression!);
                break;
            case FilterNumberOperation.GreaterThanOrEqual:
                filterExpression = Expression.GreaterThanOrEqual(property, valueExpression!);
                break;
            case FilterNumberOperation.LessThan:
                filterExpression = Expression.LessThan(property, valueExpression!);
                break;
            case FilterNumberOperation.LessThanOrEqual:
                filterExpression = Expression.LessThanOrEqual(property, valueExpression!);
                break;
            case FilterNumberOperation.Between:
                if (FilterValues!.Count != 2) {
                    throw new InvalidOperationException("Filter values count must be 2.");
                }
                filterExpression = Expression.And(
                    Expression.GreaterThanOrEqual(property, Expression.Constant(FilterValues!.First(), typeof(int))),
                    Expression.LessThanOrEqual(property, Expression.Constant(FilterValues!.Last(), typeof(int)))
                );
                break;
            case FilterNumberOperation.NotBetween:
                if (FilterValues!.Count != 2) {
                    throw new InvalidOperationException("Filter values count must be 2.");
                }
                filterExpression = Expression.Not(Expression.And(
                    Expression.LessThan(property, Expression.Constant(FilterValues!.First(), typeof(int))),
                    Expression.GreaterThan(property, Expression.Constant(FilterValues!.Last(), typeof(int)))
                ));
                break;
            default:
                throw new InvalidOperationException("Filter operation is not supported.");
        }

        var whereExpression = Expression.Lambda<Func<T, bool>>(filterExpression, parameter);
        return query.Where(whereExpression);
    }
}