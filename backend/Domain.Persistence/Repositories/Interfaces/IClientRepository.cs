using Domain.DTO.Client;
using Domain.Entities;
using Domain.Filters;
using Domain.Persistence.Repositories.Common.Interfaces;
namespace Domain.Persistence.Repositories.Interfaces;

public interface IClientRepository : IGenericRepository<Clients, int> {
    Task<int> Add(CreateClient client);
    Task<int> Add(int parentId, CreateClient client);
    Task Update(int clientId, CreateClient client);
    Task Update(int clientId, int parentId, CreateClient client);
    Task Delete(int clientId);
    Task<bool> IsClientExists(int clientId);

    Task<ICollection<Clients>> GetAll(RecordFilterPagination<ClientFilters> filter);
    Task<int> GetAllTotalPages(RecordFilterPagination<ClientFilters> filter);
    Task<int> GetAllCount(RecordFilterPagination<ClientFilters> filter);
    Task<Clients?> GetByIdWithChildren(int id);

    Task<int> AddContact(int clientId, CreateClientContact clientContact);
    Task UpdateContact(int clientId, int clientContactId, CreateClientContact clientContact);
    Task DeleteContact(int clientId, int clientContactId);
    Task<bool> IsClientContactExists(int clientId, string firstName, string lastName);

    Task<ICollection<ClientContacts>> GetClientContactsAll(int clientId, RecordFilterPagination<ClientContactsFilters> filter);
    Task<int> GetClientContactsAllTotalPages(int clientId, RecordFilterPagination<ClientContactsFilters> filter);
    Task<int> GetClientContactsAllCount(int clientId, RecordFilterPagination<ClientContactsFilters> filter);
    Task<ClientContacts?> GetClientContactById(int clientId, int clientContactId);
}