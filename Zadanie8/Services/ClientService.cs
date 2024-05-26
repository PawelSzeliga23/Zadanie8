using Zadanie8.Models;
using Zadanie8.Repositories;

namespace Zadanie8.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<Client> DeleteClientAsync(int id)
    {
        return await _clientRepository.DeleteClientAsync(id);
    }
}

public interface IClientService
{
    public Task<Client> DeleteClientAsync(int id);
}