using BLL;
using Core.Entities;
using DAL.Data;
using DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Controllers;
using Xunit;

namespace WebApi.Tests;

public class TaskControllerTests
{
    private TaskService _taskService;
    private  UserRepository _userRepository;
    private  TaskRepository _taskRepository;
    private  TaskController _taskController;
    
    public TaskControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Binary")
            .Options;

        var inMemoryDbContext = new ApplicationDbContext(options);
        
        inMemoryDbContext.Database.EnsureDeleted();
        
        _userRepository = new UserRepository(inMemoryDbContext);
        _taskRepository = new TaskRepository(inMemoryDbContext);
        
        _taskService = new TaskService(_taskRepository);

        _taskController = new TaskController(_taskService);
    }
    
    [Fact]
    public async System.Threading.Tasks.Task  DeleteTask_WhenTaskDoesntExists_ReturnBadRequest()
    {
        BadRequestObjectResult result = (BadRequestObjectResult)await _taskController.Delete(2);
        
        Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
    }

    [Fact]
    public async System.Threading.Tasks.Task  DeleteTask_WhenTaskIsDeleted_ThenStatus200()
    {
        await _taskController.Post(new Task {Id = 1, Name = "Dimas"});
        
        var result = (OkResult)await _taskController.Delete(1);
        
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
    
    
    [Fact]
    public async System.Threading.Tasks.Task  NotEndedTasksInUser_WhenTaskExists_ThenListOfTasks()
    {
        await _userRepository.Create(new User()
        {
            Id = 1
        });
        await _taskRepository.Create(new Task()
        {
            Id = 1,
            PerformerId = 1,
            State = 2
        });
        await _taskRepository.Create(new Task()
        {
            Id = 2,
            PerformerId = 1,
            State = 1
        });
        
        var result = _taskController.GetNotEndedTasksInUser(1);

        Assert.Single(await result);
    }[Fact]
    public async System.Threading.Tasks.Task  NotEndedTasksInUser_WhenUserNotExists_ThenEmptyList()
    {
        var result = _taskController.GetNotEndedTasksInUser(1);

        Assert.Empty(await result);
    }
}