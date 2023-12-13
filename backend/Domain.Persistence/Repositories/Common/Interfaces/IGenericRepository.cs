namespace Domain.Persistence.Repositories.Common.Interfaces;

public interface IGenericRepository<T> where T : class {
    Task<T?> GetById(Guid id);
}