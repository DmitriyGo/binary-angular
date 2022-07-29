using Core.Entities.LinqDTOs;
using Newtonsoft.Json;

namespace binary_linq;

internal class Program
{
    private static async Task Main(string[] args)
    {
        string? command;
        string content;
        int numberOfTask;
        int id;

        var _httpClient = new HttpClient();

        TimerWrapper tw = new TimerWrapper();
        
        Console.WriteLine("Взаимодействие с Projects, Tasks, Teams и Users доступно через swagger");
        while (true)
        {
            Console.WriteLine("Выберите желаемое действие с Linq");
            Console.WriteLine("1 - Получить кол-во тасков у проекта конкретного пользователя");
            Console.WriteLine("2 - Cписок тасков, назначенных на конкретного пользователя");
            Console.WriteLine("3 - Выполненные таски");
            Console.WriteLine("4 - Получить список из коллекции команд, участники которых старше 10 лет");
            Console.WriteLine("5 - Получить список пользователей по алфавиту first_name (по возрастанию) с отсортированными tasks по длине name (по убыванию)");
            Console.WriteLine("6 - Получить структуру 6");
            Console.WriteLine("7 - Получить структуру 7");

            command = Console.ReadLine()!;

            if (!int.TryParse(command, out numberOfTask)) Console.WriteLine("Please write a number\n");

            try
            {
                switch (numberOfTask)
                {
                    case 1:
                        var taskId = await tw.MarkRandomTaskWithDelay(1000);
                        Console.WriteLine($"Completed task id: {taskId}");
                        
                        while (true)
                        {
                            Console.WriteLine("Введите id пользователя");
                            command = Console.ReadLine();
                            if (!int.TryParse(command, out id))
                            {
                                Console.WriteLine("Некорректный ввод!");
                                continue;
                            }

                            break;
                        }

                        content = await (await _httpClient.GetAsync(
                                $"https://localhost:7252/api/Linq/CountTasksInUser?id={id}"))
                            .Content
                            .ReadAsStringAsync();

                        var tasksInUserDTO = JsonConvert.DeserializeObject<List<TasksInUserDTO>>(content);
                        Console.WriteLine("Вывод:");
                        foreach (var task in tasksInUserDTO)
                            Console.WriteLine($"Project Id: {task.Project.Id}; Tasks count: {task.TasksCount}");

                        Console.WriteLine();
                        Thread.Sleep(2000);
                        break;
                    case 2:
                        
                        taskId = await tw.MarkRandomTaskWithDelay(1000);
                        Console.WriteLine($"Completed task id: {taskId}");
                        
                        while (true)
                        {
                            Console.WriteLine("Введите id пользователя");
                            command = Console.ReadLine();
                            if (!int.TryParse(command, out id))
                            {
                                Console.WriteLine("Некорректный ввод!");
                                continue;
                            }

                            break;
                        }

                        content = await (await _httpClient.GetAsync(
                                $"https://localhost:7252/api/Linq/GetUsersTasks?id={id}"))
                            .Content
                            .ReadAsStringAsync();

                        var usersTasks = JsonConvert.DeserializeObject<List<Core.Entities.Task>>(content);
                        Console.WriteLine("Вывод:");
                        foreach (var t in usersTasks) Console.WriteLine($"{t.Id} - {t.Name} - {t.Description}");

                        Console.WriteLine();
                        Thread.Sleep(2000);
                        break;
                    case 3:
                        taskId = await tw.MarkRandomTaskWithDelay(1000);
                        Console.WriteLine($"Completed task id: {taskId}");
                        while (true)
                        {
                            Console.WriteLine("Введите id пользователя");
                            command = Console.ReadLine();
                            if (!int.TryParse(command, out id))
                            {
                                Console.WriteLine("Некорректный ввод!");
                                continue;
                            }

                            break;
                        }

                        content = await (await _httpClient.GetAsync(
                                $"https://localhost:7252/api/Linq/GetRecentlyFinishedTasks?id={id}"))
                            .Content
                            .ReadAsStringAsync();

                        var recentlyFinishedTasks =
                            JsonConvert.DeserializeObject<List<RecentlyFinishedTasksDTO>>(content);
                        Console.WriteLine("Вывод:");
                        foreach (var t in recentlyFinishedTasks) Console.WriteLine($"{t.Id} - {t.Name}");

                        Console.WriteLine();
                        Thread.Sleep(2000);
                        break;
                    case 4:
                        taskId = await tw.MarkRandomTaskWithDelay(1000);
                        Console.WriteLine($"Completed task id: {taskId}");
                        
                        content = await (await _httpClient.GetAsync("https://localhost:7252/api/Linq/GetOldTeams"))
                            .Content
                            .ReadAsStringAsync();

                        var oldTeams = JsonConvert.DeserializeObject<List<OldTeamsDTO>>(content);
                        Console.WriteLine("Вывод:");
                        foreach (var t in oldTeams)
                        {
                            Console.WriteLine($"{t.Id} - {t.TeamName}");
                            foreach (var user in t.Users) Console.WriteLine($"\t{user.FirstName} {user.LastName}");
                        }

                        Console.WriteLine();
                        Thread.Sleep(2000);
                        break;
                    case 5:
                        taskId = await tw.MarkRandomTaskWithDelay(1000);
                        Console.WriteLine($"Completed task id: {taskId}");
                        
                        content = await (await _httpClient.GetAsync(
                                "https://localhost:7252/api/Linq/GetListOfUsersWithTasks"))
                            .Content
                            .ReadAsStringAsync();

                        var usersWithTasksDto = JsonConvert.DeserializeObject<List<UserWithTasksDTO>>(content);
                        Console.WriteLine("Вывод:");
                        foreach (var userWithTasksDto in usersWithTasksDto!)
                        {
                            Console.WriteLine($"-----{userWithTasksDto.Name}-----");
                            foreach (var task in userWithTasksDto.Tasks)
                                Console.WriteLine($"\t{task.Id} - {task.Name} - {task.Description}");
                        }

                        Console.WriteLine();
                        Thread.Sleep(2000);
                        break;
                    case 6:
                        taskId = await tw.MarkRandomTaskWithDelay(1000);
                        Console.WriteLine($"Completed task id: {taskId}");
                        
                        while (true)
                        {
                            Console.WriteLine("Введите id пользователя");
                            command = Console.ReadLine();
                            if (!int.TryParse(command, out id))
                            {
                                Console.WriteLine("Некорректный ввод!");
                                continue;
                            }

                            break;
                        }

                        content = await (await _httpClient.GetAsync(
                                $"https://localhost:7252/api/Linq/GetUserStructure?userId={id}"))
                            .Content
                            .ReadAsStringAsync();

                        var userStructureDto = JsonConvert.DeserializeObject<UserStructureDTO>(content) ??
                                               throw new ArgumentNullException(
                                                   "JsonConvert.DeserializeObject<UserStructureDTO>(content)");

                        Console.WriteLine("Вывод:");
                        Console.WriteLine($"{userStructureDto.User.FirstName} {userStructureDto.User.LastName}");
                        Console.WriteLine(
                            $"{userStructureDto.LastProject.Name} - {userStructureDto.LastProject.CreatedAt}");
                        Console.WriteLine($"Общее количество тасков: {userStructureDto.TasksCount}");
                        Console.WriteLine($"Незавершенные таски: {userStructureDto.NotEndedTasks}");
                        Console.WriteLine($"Самый длинный таск: {userStructureDto.TheLongestTask.Name}");
                        Console.WriteLine(
                            $"{userStructureDto.TheLongestTask.CreatedAt} - {userStructureDto.TheLongestTask.FinishedAt}");


                        Console.WriteLine();
                        Thread.Sleep(2000);
                        break;
                    case 7:
                        taskId = await tw.MarkRandomTaskWithDelay(1000);
                        Console.WriteLine($"Completed task id: {taskId}");
                        
                        while (true)
                        {
                            Console.WriteLine("Введите id проекта");
                            command = Console.ReadLine();
                            if (!int.TryParse(command, out id))
                            {
                                Console.WriteLine("Некорректный ввод!");
                                continue;
                            }

                            break;
                        }

                        content = await (await _httpClient.GetAsync(
                                $"https://localhost:7252/api/Linq/GetProjectStructure?projId={id}"))
                            .Content
                            .ReadAsStringAsync();

                        var projectStructureDto = JsonConvert.DeserializeObject<ProjectStructureDTO>(content);

                        Console.WriteLine(
                            $"Проект: {projectStructureDto.Project.Id} - {projectStructureDto.Project.Name}");
                        Console.WriteLine(
                            $"Таск с самым длинным описанием: {projectStructureDto.TaskWithLongestDescription.Id} - {projectStructureDto.TaskWithLongestDescription.Name} - {projectStructureDto.TaskWithLongestDescription.Description}");
                        Console.WriteLine(
                            $"Таск с самым коротким именем: {projectStructureDto.TaskWithShortestName.Id} - {projectStructureDto.TaskWithShortestName.Name} - {projectStructureDto.TaskWithShortestName.Description}");
                        Console.WriteLine(
                            $"Общее кол-во пользователей в команде проекта, где или описание проекта > 20 символов или кол-во тасков < 3 == {projectStructureDto.ConcreteUsersCount}");

                        Console.WriteLine();
                        Thread.Sleep(2000);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Что-то пошло не по плану");
            }
        }
    }
}