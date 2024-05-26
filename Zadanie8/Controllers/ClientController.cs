using Microsoft.AspNetCore.Mvc;
using Zadanie8.Services;

namespace Zadanie8.Controllers;

[ApiController]
[Route("/api/client")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteClientAsync(int id)
    {
        try
        {
            var result = await _clientService.DeleteClientAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}