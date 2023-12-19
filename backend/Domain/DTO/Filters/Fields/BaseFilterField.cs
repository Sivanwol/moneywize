namespace Domain.DTO.Filters.Fields;

public abstract class BaseFilterField<TFilterOperation, TFilterValue>
    where TFilterOperation : Enum {
    private TFilterValue? filterValue;

    private ICollection<TFilterValue>? filterValues;
    public string FillterField { get; set; }
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

    public abstract IQueryable<T> ApplyFilter<T>(IQueryable<T> query) where T : class;
}