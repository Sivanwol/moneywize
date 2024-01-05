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
    public async Task<Clients?> GetByIdWithChildren(int id) {
        var client = await Context.Clients
            .Include(i => i.Children)
            .FirstOrDefaultAsync(w => w.Id == id);
        return client;
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
        var clientContactRecord = new ClientContacts {
            ClientId = clientId,
            FirstName = clientContact.FirstName,
            LastName = clientContact.LastName,
            Email = clientContact.Email,
            Phone1 = clientContact.Phone1,
            Phone2 = clientContact.Phone2,
            Fax = clientContact.Fax,
            Whatsapp = clientContact.Whatsapp,
            Address = clientContact.Address,
            City = clientContact.City,
            CountryId = clientContact.CountryId,
            State = clientContact.State,
            JobTitle = clientContact.JobTitle,
            Department = clientContact.Department,
            Company = clientContact.Company,
            PostalCode = clientContact.PostalCode,
            Notes = clientContact.Notes,
        };
        Context.ClientContacts.Add(clientContactRecord);
        await Context.Instance.SaveChangesAsync();
    }
    public async Task UpdateContact(int clientId, int clientContactId, CreateClientContact clientContact) {
        var clientContactRecord = await Context.ClientContacts.FirstOrDefaultAsync(w => w.Id == clientContactId && w.ClientId == clientId);
        if (clientContactRecord == null) {
            throw new Exception("Client contact not found");
        }

        clientContactRecord.FirstName = clientContact.FirstName;
        clientContactRecord.LastName = clientContact.LastName;
        clientContactRecord.Email = clientContact.Email;
        clientContactRecord.Phone1 = clientContact.Phone1;
        clientContactRecord.Phone2 = clientContact.Phone2;
        clientContactRecord.Fax = clientContact.Fax;
        clientContactRecord.Whatsapp = clientContact.Whatsapp;
        clientContactRecord.Address = clientContact.Address;
        clientContactRecord.City = clientContact.City;
        clientContactRecord.CountryId = clientContact.CountryId;
        clientContactRecord.State = clientContact.State;
        clientContactRecord.JobTitle = clientContact.JobTitle;
        clientContactRecord.Department = clientContact.Department;
        clientContactRecord.Company = clientContact.Company;
        clientContactRecord.PostalCode = clientContact.PostalCode;
        clientContactRecord.Notes = clientContact.Notes;
        await Context.Instance.SaveChangesAsync();
    }
    public async Task DeleteContact(int clientId, int clientContactId) {
        var clientContactRecord = await Context.ClientContacts.FirstOrDefaultAsync(w => w.Id == clientContactId && w.ClientId == clientId);
        Context.Instance.Remove(clientContactRecord);
        await Context.Instance.SaveChangesAsync();
    }
    public async Task<ICollection<ClientContacts>> GetClientContactsAll(int clientId, RecordFilterPagination<ClientContactsFilters> filter) {
        var query = Context.ClientContacts.AsQueryable();
        query = query.Where(w => w.DeletedAt == null && w.ClientId == clientId);
        query = filter.ApplyQuery(query);
        return await query.ToListAsync();
    }
    public async Task<int> GetClientContactsAllTotalPages(int clientId, RecordFilterPagination<ClientContactsFilters> filter) {
        var query = Context.ClientContacts.AsQueryable();
        query = query.Where(w => w.DeletedAt == null && w.ClientId == clientId);
        query = filter.ApplyQuery(query);
        return (int)Math.Ceiling((double)await query.CountAsync() / filter.PageSize);
    }
    public async Task<int> GetClientContactsAllCount(int clientId, RecordFilterPagination<ClientContactsFilters> filter) {
        var query = Context.ClientContacts.AsQueryable();
        query = query.Where(w => w.DeletedAt == null && w.ClientId == clientId);
        query = filter.ApplyQuery(query);
        return await query.CountAsync();
    }
    public async Task<Clients?> GetById(int id) {
        var client = await Context.Clients.FirstOrDefaultAsync(w => w.Id == id);
        return client;
    }
    public async Task<ClientContacts?> GetClientContactById(int clientId, int clientContactId) {
        var clientContact = await Context.ClientContacts.FirstOrDefaultAsync(w => w.Id == clientContactId && w.ClientId == clientId);
        return clientContact;
    }
    public async Task<bool> IsClientContactExists(int clientId, string firstName, string lastName) {
        var clientContact =
            await Context.ClientContacts.FirstOrDefaultAsync(w => w.ClientId == clientId && w.DeletedAt == null && w.FirstName == firstName && w.LastName == lastName);
        return clientContact != null;
    }
}