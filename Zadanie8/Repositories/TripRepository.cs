using Microsoft.EntityFrameworkCore;
using Zadanie8.Context;
using Zadanie8.Controllers;
using Zadanie8.DTO;
using Zadanie8.Exceptions;
using Zadanie8.Models;

namespace Zadanie8.Repositories;

public class TripRepository : ITripRepository
{
    private readonly MasterContext _context;

    public TripRepository(MasterContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TripDto>> GetTripsAsync()
    {
        var trips = await _context.Trips
            .Include(t => t.IdCountries)
            .ThenInclude(tc => tc.IdCountry)
            .Include(t => t.ClientTrips)
            .ThenInclude(ct => ct.IdClientNavigation)
            .Select(t => new TripDto
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Clients = t.ClientTrips.Select(ct => new ClientDto
                {
                    FirstName = ct.IdClientNavigation.FirstName,
                    LastName = ct.IdClientNavigation.LastName
                }),
                Countries = _context.Countries.Where(c => t.IdCountries.Any(tc => tc.IdCountry == c.IdCountry))
                    .Select(c => new CountryDto
                    {
                        Name = c.Name
                    })
            }).OrderByDescending(t => t.DateFrom).ToListAsync();
        return trips;
    }

    public async Task<Trip> AddClientToTripAsync(int id, ClientInDto clientDto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var newId = 0;
            var client = await _context.Clients.FindAsync(clientDto.Pesel);
            if (CheckIfClientExists(client))
            {
                newId = client!.IdClient;
            }
            else
            {
                newId = _context.Clients.Max(c => c.IdClient) + 1;
                _context.Clients.Add(new Client
                {
                    IdClient = newId,
                    Pesel = clientDto.Pesel!,
                    FirstName = clientDto.FirstName!,
                    LastName = clientDto.LastName!,
                    Email = clientDto.Email!,
                    Telephone = clientDto.Telephone!
                });
            }

            var trip = await _context.Trips.FindAsync(id);
            CheckIfTripExists(trip);

            var clientTrip =
                _context.ClientTrips.FirstOrDefault(ct => ct.IdClient == newId && ct.IdTrip == id);
            CheckIfClientInTrip(clientTrip);
            
            _context.ClientTrips.Add(new ClientTrip
            {
                IdClient = newId,
                IdTrip = id,
                RegisteredAt = DateTime.Now,
                PaymentDate = clientDto.PaymentDate
            });

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return trip;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new BadRequestException(e.Message);
        }
    }

    private void CheckIfTripExists(Trip? trip)
    {
        if (trip == null)
        {
            throw new NotFoundException("Wycieczka nie istnieje");
        }
    }

    private bool CheckIfClientExists(Client? client)
    {
        return client != null;
    }

    private void CheckIfClientInTrip(ClientTrip? clientTrip)
    {
        if (clientTrip != null)
        {
            throw new BadRequestException("Klient jest już zapisany na wycieczkę");
        }
    }
}

public interface ITripRepository
{
    public Task<IEnumerable<TripDto>> GetTripsAsync();
    public Task<Trip> AddClientToTripAsync(int id, ClientInDto clientDto);
}