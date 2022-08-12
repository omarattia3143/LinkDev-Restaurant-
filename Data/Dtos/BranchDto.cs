using System.ComponentModel.DataAnnotations;

namespace LinkDev.EgyptianRecipes.Data.Dtos;

/// <summary>
/// I am showing that Dtos needs to be used but as for this project there is no properties to hide from the end-user
/// </summary>
public class BranchDto
{
    public int Id { get; set; }
    public TimeSpan OpeningTime { get; set; }
    public TimeSpan ClosingTime { get; set; }
    [MaxLength(200)]
    public string Title { get; set; }
    [MaxLength(250)]
    public string ManagerName { get; set; }
}