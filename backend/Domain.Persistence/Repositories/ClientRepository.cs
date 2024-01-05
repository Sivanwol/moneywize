using Domain.DTO.Client;
using Domain.Entities;
using Domain.Filters;
using Domain.Persistence.Context;
using Domain.Persistence.Repositories.Common;
using Domain.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Domain.Persistence.Repositories;

public class ClientRepository : BaseRepository, IClientRepository {
    public ClientRepository(
        IDomainContext context
    ) : base(context) { }
    public async Task Add(CreateClient client) {
        var clientRecord = new Clients {
            OwnerUserId = client.OwnerUserId,
            Parent = null,
            Name = client.Name,
            Description = client.Description,
            Website = client.Website,
            CountryId = client.CountryId,
            Address = client.Address,
            City = client.City,

        };
        Context.Clients.Add(clientRecord);
        await Context.Instance.SaveChangesAsync();
    }
    public async Task Add(int parantId, CreateClient client) {
        var clientRecord = new Clients {
            OwnerUserId = client.OwnerUserId,
            ParentId = parantId,
            Name = client.Name,
            Description = client.Description,
            Website = client.Website,
            CountryId = client.CountryId,
            Address = client.Address,
            City = client.City,

        };
        Context.Clients.Add(clientRecord);
        await Context.Instance.SaveChangesAsync();
    }

    public async Task Update(int clientId, CreateClient client) {
        var clientRecord = await Context.Clients.FirstOrDefaultAsync(w => w.Id == clientId);
        if (clientRecord == null) {
            throw new Exception("Client not found");
        }
        clientRecord.OwnerUserId = client.OwnerUserId;
        clientRecord.Name = client.Name;
        clientRecord.Description = client.Description;
        clientRecord.Website = client.Website;
        clientRecord.CountryId = client.CountryId;
        clientRecord.Address = client.Address;
        clientRecord.City = client.City;
        await Context.Instance.SaveChangesAsync();
    }
    public async Task Delete(int clientId) {
        var clientRecord = await Context.Clients.FirstOrDefaultAsync(w => w.Id == clientId);
        Context.Instance.Remove(clientRecord);
        await Context.Instance.SaveChangesAsync();
    }
    public async Task<bool> IsClientExists(int clientId) {
        var client = await Context.Clients.FirstOrDefaultAsync(w => w.Id == clientId);
        return client != null;
    }
    public async Task<ICollection<Clients>> GetAll(RecordFilterPagination<ClientFilters> filter) {
        var query = Context.Clients.AsQueryable();
        query = query.Where(w => w.DeletedAt == null);
        query = filter.ApplyQuery(query);
        return await query.ToListAsync();
    }
    public async Task<int> GetAllTotalPages(RecordFilterPagination<ClientFilters> filter) {
        var query = Context.Clients.AsQueryable();
        query = query.Where(w => w.DeletedAt == null);
        var entitiesCount = await filter.ApplyQueryWithoutPagination(query).CountAsync();
        return (int)Math.Ceiling((double)entitiesCount / filter.PageSize);
    }
    public async Task<int> GetAllCount(RecordFilterPagination<ClientFilters> filter) {
        var query = Context.Clients.AsQueryable();
        query = query.Where(w => w.DeletedAt == null);
        var entitiesCount = await filter.ApplyQueryWithoutPagination(query).CountAsync();
        return entitiesCount;
    }
    public async Task AddContact(int clientId, CreateClientContact clientContact) {
        throw new NotImplementedException();
    }
    public async Task UpdateContact(int clientId, CreateClientContact clientContact) {
        throw new NotImplementedException();
    }
    public async Task DeleteContact(int clientId) {
        throw new NotImplementedException();
    }
    public async Task<ICollection<Clients>> GetClientContactsAll(RecordFilterPagination<ClientFilters> filter) {
        throw new NotImplementedException();
    }
    public async Task<int> GetClientContactsAllTotalPages(RecordFilterPagination<ClientFilters> filter) {
        throw new NotImplementedException();
    }
    public async Task<int> GetClientContactsAllCount(RecordFilterPagination<ClientFilters> filter) {
        throw new NotImplementedException();
    }
    public async Task<Clients?> GetById(int id) {
        var client = await DomainContext.Clients.FirstOrDefaultAsync(w => w.Id == id);
        return client;
    }
}