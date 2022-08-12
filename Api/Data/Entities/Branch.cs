using System.ComponentModel.DataAnnotations;

namespace LinkDev.EgyptianRecipes.Data.Entities;

public class Branch
{
    [Required] public int Id { get; set; }
    [Required] public TimeSpan OpeningTime { get; set; }
    [Required] public TimeSpan ClosingTime { get; set; }
    [Required] [MaxLength(200)] public string Title { get; set; }
    [Required] [MaxLength(250)] public string ManagerName { get; set; }
}