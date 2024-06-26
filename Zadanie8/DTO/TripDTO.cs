﻿using Zadanie8.Models;

namespace Zadanie8.DTO;

public class TripDto
{
    public string? Name { get; set; } = null!;

    public string? Description { get; set; } = null!;

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public int MaxPeople { get; set; }
    public IEnumerable<ClientDto> Clients { get; set; } = null!;
    public IEnumerable<CountryDto> Countries { get; set; } = null!;
}