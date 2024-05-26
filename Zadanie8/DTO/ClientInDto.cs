using System.ComponentModel.DataAnnotations;

namespace Zadanie8.Controllers;

public class ClientInDto
{
    [Required] public string? FirstName { get; set; }
    [Required] public string? LastName { get; set; }
    [Required] [EmailAddress] public string? Email { get; set; }
    [Required] [Phone] public string? Telephone { get; set; }
    [Required] public string? Pesel { get; set; }
    [Required] public int? IdTrip { get; set; }
    [Required] public string? TripName { get; set; }
    [Required] public DateTime? PaymentDate { get; set; }
}