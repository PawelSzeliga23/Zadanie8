using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Zadanie8.Context;
using Zadanie8.Exceptions;
using Zadanie8.Models;

namespace Zadanie8.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly MasterContext _context;

    public ClientRepository(MasterContext context)
    {
        _context = context;
    }

    public async Task<Client> DeleteClientAsync(int id)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var client = await _context.Clients.FindAsync(id);
            CheckIfClientExists(client);

            var trips = await _context.ClientTrips.Where(ct => ct.IdClient == id).ToListAsync();
            CheckIfClientHasTrips(trips);

            _context.Clients.Remove(client!);
            await transaction.CommitAsync();
            return client!;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new BadRequestException(e.Message);
        }
    }

    private void CheckIfClientExists(Client? client)
    {
        if (client == null)
        {
            throw new NotFoundException($"Klient nie istnieje");
        }
    }

    private void CheckIfClientHasTrips(List<ClientTrip> trips)
    {
        if (trips.Count > 0)
        {
            throw new BadRequestException("Nie można usunąć klienta, ponieważ ma przypisane wycieczki");
        }
    }
}

public interface IClientRepository
{
    public Task<Client> DeleteClientAsync(int id);
}