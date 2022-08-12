namespace LinkDev.EgyptianRecipes.Data.Dtos;

/// <summary>
/// I could have used tuple but this is clearer
/// </summary>
public class BranchShift
{
    public TimeSpan OpeningTime { get; set; }
    public TimeSpan ClosingTime { get; set; }
}