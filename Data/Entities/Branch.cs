using System.ComponentModel.DataAnnotations;

namespace LinkDev.EgyptianRecipes.Data.Entities;

public class Branch
{
    public int Id { get; set; }
    public TimeSpan OpeningTime { get; set; }
    public TimeSpan ClosingTime { get; set; }
    [MaxLength(200)]
    public string Title { get; set; }
    [MaxLength(250)]
    public string ManagerName { get; set; }
}