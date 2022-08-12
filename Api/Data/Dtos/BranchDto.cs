using System.ComponentModel.DataAnnotations;

namespace LinkDev.EgyptianRecipes.Data.Dtos;

/// <summary>
/// I am showing that Dtos needs to be used but as for this project there is no properties to hide from the end-user
/// </summary>
public class BranchDto
{
    [Required] public int Id { get; set; }
    [Required] public TimeSpan OpeningTime { get; set; }
    [Required] public TimeSpan ClosingTime { get; set; }
    [Required] [MaxLength(200)] public string Title { get; set; }
    [Required] [MaxLength(250)] public string ManagerName { get; set; }
}