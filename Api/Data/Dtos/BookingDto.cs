using System.ComponentModel.DataAnnotations;
using LinkDev.EgyptianRecipes.Data.Entities;

namespace LinkDev.EgyptianRecipes.Data.Dtos;

/// <summary>
/// I am showing that Dtos needs to be used but as for this project there is no properties to hide from the end-user
/// </summary>
public class BookingDto
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