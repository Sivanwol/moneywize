using System.Linq.Expressions;
namespace Domain.DTO.Filters.Fields;

public interface IFilterField<T> {
    public bool IsFilterDisabled { get; set; }
    public Expression<Func<T, bool>> ApplyFilter<T>();
}