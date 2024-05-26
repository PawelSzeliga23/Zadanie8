using Zadanie8.Controllers;
using Zadanie8.DTO;
using Zadanie8.Models;
using Zadanie8.Repositories;

namespace Zadanie8.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;

    public TripService(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async Task<IEnumerable<TripDto>> GetTripsAsync()
    {
        return await _tripRepository.GetTripsAsync();
    }

    public async Task<Trip> AddClientToTripAsync(int id, ClientInDto clientDto)
    {
        return await _tripRepository.AddClientToTripAsync(id, clientDto);
    }
}

public interface ITripService
{
    public Task<IEnumerable<TripDto>> GetTripsAsync();
    public Task<Trip> AddClientToTripAsync(int id, ClientInDto clientDto);
}