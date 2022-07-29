using System;
using System.Collections.Generic;
using Core.Entities;
using DAL.Data;
using DAL.Interfaces;
using DAL.Repositories;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Xunit;

namespace BLL.Tests;

public class TaskServiceTest
{
    private TaskService _taskService;

    private  TaskRepository _taskRepository;
    private  UserRepository _userRepository;
    
    public TaskServiceTest()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Binary")
            .Options;

        var inMemoryDbContext = new ApplicationDbContext(options);
        
        inMemoryDbContext.Database.EnsureDeleted();

        _taskRepository = new TaskRepository(inMemoryDbContext);
        _userRepository = new UserRepository(inMemoryDbContext);
        
        _taskService = new TaskService(_taskRepository);
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Task_Change_As_Completed()
    {
        await _taskRepository.Create(new Task()
        {
            Id = 1,
            Name = "asd",
            Description = "qwe",
            State = 1,
            PerformerId = 1,
            ProjectId = 1,
            CreatedAt = new DateTime(2009,12,14,14,15,0),
            FinishedAt = new DateTime(2021,10,9,9,0,0)
        });

        var task = _taskRepository.GetById(1);
        (await task).State = 2;
        
        await _taskService.Update(await task);
    
        Assert.Equal(2, (await _taskRepository.GetById(1)).State);
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Task_Change_As_Not_Allowed()
    {
        await _taskRepository.Create(new Task()
        {
            Id = 1,
            Name = "asd",
            Description = "qwe",
            State = 1,
            PerformerId = 1,
            ProjectId = 1,
            CreatedAt = new DateTime(2009,12,14,14,15,0),
            FinishedAt = new DateTime(2021,10,9,9,0,0)
        });

        var task = _taskRepository.GetById(1);
        (await task).State = 5;
        
        await Assert.ThrowsAsync<ArgumentException>(async () => await _taskService.Update(await task));
    }
    
    
    
    [Fact]
    public async System.Threading.Tasks.Task NotEndedTasksInUser()
    {
        await _userRepository.Create(new User()
        {
            Id = 1
        });
        
        await _taskRepository.Create(new Task()
        {
            Id = 1,
            State = 1,
            PerformerId = 1,
        }); 
        await _taskRepository.Create(new Task()
        {
            Id = 2,
            State = 0,
            PerformerId = 1,
        });
        await _taskRepository.Create(new Task()
        {
            Id = 3,
            State = 2,
            PerformerId = 1,
        });

        var tasks = _taskService.GetNotEndedTasksInUser(1);
        
    
        Assert.Equal(2, (await tasks).Count);
    }
    [Fact]
    public async System.Threading.Tasks.Task NotEndedTasksInUser_Incorrect()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _taskService.GetNotEndedTasksInUser(1));
    }
}