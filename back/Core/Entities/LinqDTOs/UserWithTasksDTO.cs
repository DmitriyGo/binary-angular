namespace Core.Entities.LinqDTOs;

public class UserWithTasksDTO
{
    public string? Name { get; set; }
    public List<Core.Entities.Task> Tasks { get; set; }
}