using System.Linq.Expressions;
namespace Domain.DTO.Filters.Fields;

public class DateFilterField : BaseFilterField<FilterDateOperation, DateTime> {

    public DateFilterField(string filterField, FilterDateOperation operation) : base(filterField, operation) { }
    public DateFilterField(string filterField, FilterDateOperation operation, bool isDisabled) : base(filterField, operation) {
        IsFilterDisabled = isDisabled;
    }


    public override Expression<Func<T, bool>> ApplyFilter<T>() {
        if (IsFilterDisabled) {
            return null;
        }
        Expression valuesExpression = null;
        Expression filterExpression = null;
        Expression valueExpression = null;
        // Accessing the property with provided field name
        var parameter = Expression.Parameter(typeof(T), "t");
        var property = Expression.Property(parameter, FilterField); // Creating filter expression based on operation and type of TFilterValue
        if (FilterOperation is FilterDateOperation.Between or FilterDateOperation.NotBetween or FilterDateOperation.In or FilterDateOperation.NotIn) {
            if (FilterValues == null) {
                throw new InvalidOperationException("Filter values are not provided.");
            }
            valuesExpression = Expression.Constant(FilterValues, typeof(ICollection<DateTime>));
        } else {
            if (FilterValue == null) {
                throw new InvalidOperationException("Filter value is not provided.");
            }
            valueExpression = Expression.Constant(FilterValue, typeof(DateTime));
        }
        switch (FilterOperation) {
            case FilterDateOperation.In:
                filterExpression = Expression.Call(typeof(Enumerable), nameof(Enumerable.Contains), new[] {
                    typeof(DateTime)
                }, valuesExpression!, property);
                break;
            case FilterDateOperation.NotIn:
                filterExpression = Expression.Not(Expression.Call(typeof(Enumerable), nameof(Enumerable.Contains), new[] {
                    typeof(DateTime)
                }, valuesExpression!, property));
                break;
            case FilterDateOperation.Equal:
                filterExpression = Expression.Equal(property, valueExpression!);
                break;
            case FilterDateOperation.NotEqual:
                filterExpression = Expression.NotEqual(property, valueExpression!);
                break;
            case FilterDateOperation.GreaterThan:
                filterExpression = Expression.GreaterThan(property, valueExpression!);
                break;
            case FilterDateOperation.GreaterThanOrEqual:
                filterExpression = Expression.GreaterThanOrEqual(property, valueExpression!);
                break;
            case FilterDateOperation.LessThan:
                filterExpression = Expression.LessThan(property, valueExpression!);
                break;
            case FilterDateOperation.LessThanOrEqual:
                filterExpression = Expression.LessThanOrEqual(property, valueExpression!);
                break;
            case FilterDateOperation.Between:
                if (FilterValues!.Count != 2) {
                    throw new InvalidOperationException("Filter values count must be 2.");
                }
                filterExpression = Expression.And(
                    Expression.GreaterThanOrEqual(property, Expression.Constant(FilterValues!.First(), typeof(DateTime))),
                    Expression.LessThanOrEqual(property, Expression.Constant(FilterValues!.Last(), typeof(DateTime)))
                );
                break;
            case FilterDateOperation.NotBetween:
                if (FilterValues!.Count != 2) {
                    throw new InvalidOperationException("Filter values count must be 2.");
                }
                filterExpression = Expression.Not(Expression.And(
                    Expression.LessThan(property, Expression.Constant(FilterValues!.First(), typeof(DateTime))),
                    Expression.GreaterThan(property, Expression.Constant(FilterValues!.Last(), typeof(DateTime)))
                ));
                break;
            default:
                throw new InvalidOperationException("Filter operation is not supported.");
        }


        return Expression.Lambda<Func<T, bool>>(filterExpression, parameter);
    }
}