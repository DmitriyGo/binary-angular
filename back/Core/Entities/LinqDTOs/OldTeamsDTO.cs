namespace Core.Entities.LinqDTOs;

public class OldTeamsDTO
{
    public int Id { get; set; }
    public string TeamName { get; set; }
    public User[] Users { get; set; }
}