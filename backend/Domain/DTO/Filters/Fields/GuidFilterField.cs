using System.Linq.Expressions;
namespace Domain.DTO.Filters.Fields;

public class GuidFilterField : BaseFilterField<FilterGuidOperation, Guid> {

    public GuidFilterField(string fillterField) : base(fillterField) { }
    public GuidFilterField(string fillterField, bool isDisabled) : base(fillterField) {
        IsFilterDisabled = isDisabled;
    }
    public override IQueryable<T> ApplyFilter<T>(IQueryable<T> query) {
        if (IsFilterDisabled) {
            return query;
        }
        Expression valuesExpression = null;
        Expression filterExpression = null;
        Expression valueExpression = null;
        // Accessing the property with provided field name
        var parameter = Expression.Parameter(typeof(T), "t");
        var property = Expression.Property(parameter, FillterField); // Creating filter expression based on operation and type of TFilterValue
        if (FilterOperation is FilterGuidOperation.In or FilterGuidOperation.NotIn) {
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
            case FilterGuidOperation.In:
                filterExpression = Expression.Call(typeof(Enumerable), nameof(Enumerable.Contains), new[] {
                    typeof(int)
                }, valuesExpression!, property);
                break;
            case FilterGuidOperation.NotIn:
                filterExpression = Expression.Not(Expression.Call(typeof(Enumerable), nameof(Enumerable.Contains), new[] {
                    typeof(int)
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

        var whereExpression = Expression.Lambda<Func<T, bool>>(filterExpression, parameter);
        return query.Where(whereExpression);
    }
}