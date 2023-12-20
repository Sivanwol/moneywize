using System.Linq.Expressions;
namespace Domain.DTO.Filters.Fields;

public class GuidFilterField : BaseFilterField<FilterGuidOperation, Guid> {

    public GuidFilterField(string filterField, FilterGuidOperation operation) : base(filterField, operation) { }
    public GuidFilterField(string filterField, FilterGuidOperation operation, bool isDisabled) : base(filterField, operation) {
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
        if (FilterOperation is FilterGuidOperation.In or FilterGuidOperation.NotIn) {
            if (FilterValues == null) {
                throw new InvalidOperationException("Filter values are not provided.");
            }
            valuesExpression = Expression.Constant(FilterValues, typeof(ICollection<Guid>));
        } else {
            if (FilterValue == null) {
                throw new InvalidOperationException("Filter value is not provided.");
            }
            valueExpression = Expression.Constant(FilterValue, typeof(Guid));
        }
        switch (FilterOperation) {
            case FilterGuidOperation.In:
                filterExpression = Expression.Call(typeof(Enumerable), nameof(Enumerable.Contains), new[] {
                    typeof(Guid)
                }, valuesExpression!, property);
                break;
            case FilterGuidOperation.NotIn:
                filterExpression = Expression.Not(Expression.Call(typeof(Enumerable), nameof(Enumerable.Contains), new[] {
                    typeof(Guid)
                }, valuesExpression!, property));
                break;
            case FilterGuidOperation.Equal:
                filterExpression = Expression.Equal(property, valueExpression!);
                break;
            case FilterGuidOperation.NotEqual:
                filterExpression = Expression.NotEqual(property, valueExpression!);
                break;
            default:
                throw new InvalidOperationException("Filter operation is not supported.");
        }

        return Expression.Lambda<Func<T, bool>>(filterExpression, parameter);
    }
}