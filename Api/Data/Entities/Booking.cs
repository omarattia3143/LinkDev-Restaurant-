using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinkDev.EgyptianRecipes.Data.Entities;

public class Booking
{
    [Required] public int Id { get; set; }
    [Required] [MaxLength(500)] public string Username { get; set; }

    [Required]
    [RegularExpression(@"^[0-9]*$", ErrorMessage = "Not a valid phone number")]
    public string Phone { get; set; }

    [Required] public int NumberOfChairs { get; set; }
    [Required] public DateTime BookingStartDateTime { get; set; }
    [Required] public DateTime BookingEndDateTime { get; set; }
    
    [Required] public int BranchId { get; set; } 
    public Branch Branch { get; set; }
    
    
    
}