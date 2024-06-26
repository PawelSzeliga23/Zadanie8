﻿using Microsoft.AspNetCore.Mvc;
using Zadanie8.Services;

namespace Zadanie8.Controllers;

[ApiController]
[Route("/api/trip")]
public class TripController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTripsAsync()
    {
        var trips = await _tripService.GetTripsAsync();
        return Ok(trips);
    }

    [HttpPost]
    [Route("{id}/clients")]
    public async Task<IActionResult> AddClientToTripAsync(int id, [FromBody] ClientInDto clientDto)
    {
        try
        {
            var result = await _tripService.AddClientToTripAsync(id, clientDto);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}