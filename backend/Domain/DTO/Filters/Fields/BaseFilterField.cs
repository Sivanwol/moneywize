using System.Linq.Expressions;
namespace Domain.DTO.Filters.Fields;

public abstract class BaseFilterField<TFilterOperation, TFilterValue> : IFilterField<TFilterValue>
    where TFilterOperation : Enum {
    private TFilterValue? filterValue;
    private ICollection<TFilterValue>? filterValues;
    public BaseFilterField(string filterField, TFilterOperation operation) {
        FilterField = filterField;
        FilterOperation = operation;
    }
    public string FilterField { get; }
    public TFilterOperation FilterOperation { get; set; }

    public TFilterValue? FilterValue
    {
        get { return filterValue; }
        set
        {
            if (FilterValues != null) {
                throw new ArgumentException("Both value and collection can't be filled at the same time");
            }
            filterValue = value;
        }
    }

    public ICollection<TFilterValue>? FilterValues
    {
        get { return filterValues; }
        set
        {
            if (value != null) {
                throw new ArgumentException("Both value and collection can't be filled at the same time");
            }
            filterValues = value;
        }
    }

    public bool IsFilterDisabled { get; set; } = false;

    public abstract Expression<Func<T, bool>> ApplyFilter<T>();
}