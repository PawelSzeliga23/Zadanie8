using Zadanie8.Controllers;
using Zadanie8.DTO;
using Zadanie8.Models;
using Zadanie8.Repositories;

namespace Zadanie8.Services;

public class TripService : ITripService
{
    private readonly TripRepository _tripRepository;

    public TripService(TripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async Task<IEnumerable<TripDto>> GetTripsAsync()
    {
        return await _tripRepository.GetTripsAsync();
    }

    public async Task<Trip> AddClientToTripAsync(int id, ClientInDto clientDto)
    {
        throw new NotImplementedException();
    }
}

public interface ITripService
{
    public Task<IEnumerable<TripDto>> GetTripsAsync();
    public Task<Trip> AddClientToTripAsync(int id, ClientInDto clientDto);
}