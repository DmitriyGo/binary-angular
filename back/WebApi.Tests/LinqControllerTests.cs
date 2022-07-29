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

public class LinqControllerTests
{
    private LinqService _linqService;

    private readonly ProjectRepository _projectRepository;
    private readonly TeamRepository _teamRepository;
    private readonly TaskRepository _taskRepository;
    private readonly UserRepository _userRepository;
    
    private  LinqController _linqController;
    
    public LinqControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Binary")
            .Options;

        var inMemoryDbContext = new ApplicationDbContext(options);
        
        inMemoryDbContext.Database.EnsureDeleted();

        _projectRepository = new ProjectRepository(inMemoryDbContext);
        _teamRepository = new TeamRepository(inMemoryDbContext);
        _taskRepository = new TaskRepository(inMemoryDbContext);
        _userRepository = new UserRepository(inMemoryDbContext);
        
        _linqService = new LinqService(_projectRepository, 
            _teamRepository,
            _taskRepository,
            _userRepository);

        _linqController = new LinqController(_linqService);
    }
    
    [Fact]
    public async System.Threading.Tasks.Task  CountTasksInUser_WhenUserExists_ReturnListOfTasksInUserDTO()
    {
        await _userRepository.Create(new User()
        {
            Id = 1
        });
        await _taskRepository.Create(new Task()
        {
            Id = 1,
            PerformerId = 1
        });
        await _taskRepository.Create(new Task()
        {
            Id = 2,
            PerformerId = 1
        });
        var result = _linqController.CountTasksInUser(1);
        
        Assert.Equal(2, (await result)[0].TasksCount);
    }

    [Fact]
    public async System.Threading.Tasks.Task  CountTasksInUser_WhenUserDoesntExists_ReturnEmptyList()
    {
        var result = _linqController.CountTasksInUser(1);
        
        Assert.Empty(await result);
    }
}