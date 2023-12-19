using System.Linq.Expressions;
namespace Domain.DTO.Filters.Fields;

public class BoolFilterField : BaseFilterField<FilterBooleanOperation, bool> {

    public override IQueryable<T> ApplyFilter<T>(IQueryable<T> query) {
        Expression filterExpression = null;
        Expression valueExpression = null;
        // Accessing the property with provided field name
        var parameter = Expression.Parameter(typeof(T), "t");
        var property = Expression.Property(parameter, FillterField); // Creating filter expression based on operation and type of TFilterValue

        valueExpression = Expression.Constant(FilterValue, typeof(bool));
        switch (FilterOperation) {
            case FilterBooleanOperation.Equal:
                filterExpression = Expression.Equal(property, valueExpression!);
                break;
            case FilterBooleanOperation.NotEqual:
                filterExpression = Expression.NotEqual(property, valueExpression!);
                break;
            default:
                throw new InvalidOperationException("Filter operation is not supported.");
        }

        var whereExpression = Expression.Lambda<Func<T, bool>>(filterExpression, parameter);
        return query.Where(whereExpression);
    }
}