namespace Core.Entities.LinqDTOs;
//(User user, Task lastTask, int tasksCount, int notEndedTasks, Task theLongestTask)
public class UserStructureDTO
{
    public User User { get; set; }
    public Project LastProject { get; set; }
    public int TasksCount { get; set; }
    public int NotEndedTasks { get; set; }
    public Core.Entities.Task TheLongestTask { get; set; }
}