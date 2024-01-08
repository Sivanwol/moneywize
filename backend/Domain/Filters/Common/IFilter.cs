using Domain.DTO.ConfigurableEntities;
using Infrastructure.Enums;
namespace Domain.Filters.Common;

public interface IFilter {
    public IQueryable<T> ApplyFilters<T>(IQueryable<T> query, FilterCollectionOperation filterCollectionOperation, IEnumerable<ConfigurableUserViewColumn> SelectedColumns)
        where T : class;
    public void ResetFilters();
}