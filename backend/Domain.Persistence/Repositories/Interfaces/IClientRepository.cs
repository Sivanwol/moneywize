using Domain.DTO.Client;
using Domain.Entities;
using Domain.Filters;
using Domain.Persistence.Repositories.Common.Interfaces;
namespace Domain.Persistence.Repositories.Interfaces;

public interface IClientRepository : IGenericRepository<Clients, int> {
    Task Add(CreateClient client);
    Task Add(int parantId, CreateClient client);
    Task Update(int clientId, CreateClient client);
    Task Delete(int clientId);
    Task<bool> IsClientExists(int clientId);

    Task<ICollection<Clients>> GetAll(RecordFilterPagination<ClientFilters> filter);
    Task<int> GetAllTotalPages(RecordFilterPagination<ClientFilters> filter);
    Task<int> GetAllCount(RecordFilterPagination<ClientFilters> filter);

    Task AddContact(int clientId, CreateClientContact clientContact);
    Task UpdateContact(int clientId, CreateClientContact clientContact);
    Task DeleteContact(int clientId);

    Task<ICollection<Clients>> GetClientContactsAll(RecordFilterPagination<ClientFilters> filter);
    Task<int> GetClientContactsAllTotalPages(RecordFilterPagination<ClientFilters> filter);
    Task<int> GetClientContactsAllCount(RecordFilterPagination<ClientFilters> filter);
}