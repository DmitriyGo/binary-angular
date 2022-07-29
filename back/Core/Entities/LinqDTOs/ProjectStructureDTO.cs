namespace Core.Entities.LinqDTOs;
//(Project project, Task longestDescTask, Task shortestNameTask, int concreteUsersCount)
public class ProjectStructureDTO
{
    public Project Project { get; set; }
    public Core.Entities.Task TaskWithLongestDescription { get; set; }
    public Core.Entities.Task TaskWithShortestName { get; set; }
    public int ConcreteUsersCount { get; set; }
}