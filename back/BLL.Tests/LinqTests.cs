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

public class LinqServiceTest : IDisposable
{
    private LinqService _linqService;

    private  ProjectRepository _projectRepository;
    private  TeamRepository _teamRepository;
    private  TaskRepository _taskRepository;
    private  UserRepository _userRepository;
    
    public LinqServiceTest()
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
    }
    
    public void Dispose()
    {
        _linqService.Dispose();
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Linq1_Correct_Input()
    {
        await _userRepository.Create(new User()
        {
            Id = 1,
            TeamId = 1
        });
        
        await _teamRepository.Create(A.Fake<Team>());
        
        await _projectRepository.Create(new Project()
        {
            Id = 1,
            AuthorId = 1,
            TeamId = 1,
            Tasks = new List<Task>()
            {
                new Task()
                {
                    Id = 1,
                    ProjectId = 1,
                    PerformerId = 1,
                },
                new Task()
                {
                    Id = 2,
                    ProjectId = 1,
                    PerformerId = 1,
                },
                
            }
        });
        
        Assert.Equal(2, (await _linqService.CountTasksInUser(1))[0].TasksCount);
    }

    [Fact]
    public async System.Threading.Tasks.Task Linq1_Incorrect_Input()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _linqService.CountTasksInUser(-1));
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Linq2_3_Tasks_ButOneLargerThanNeeded()
    {
        await _userRepository.Create(new User()
        {
            Id = 1,
            TeamId = 1
        });
        
        await _teamRepository.Create(A.Fake<Team>());
        
        await _projectRepository.Create(new Project()
        {
            Id = 1,
            AuthorId = 1,
            TeamId = 1,
            Tasks = new List<Task>()
            {
                new Task()
                {
                    Id = 1,
                    ProjectId = 1,
                    PerformerId = 1,
                    Name = new String('a', 5)
                },
                new Task()
                {
                    Id = 2,
                    ProjectId = 1,
                    PerformerId = 1,
                    Name = new String('b', 8)
                },
                new Task()
                {
                    Id = 3,
                    ProjectId = 1,
                    PerformerId = 1,
                    Name = new String('a', 46)
                },
                
            }
        });
        
        var b = _linqService.GetUsersTasks(1);
        Assert.Equal(2, (await b).Count);
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Linq2_Incorrect_Input()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _linqService.GetUsersTasks(-1));
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Linq3_3_Tasks_ButOnlyTwoEndedIn2022()
    {
        
        await _userRepository.Create(new User()
        {
            Id = 1,
            TeamId = 1
        });
        
        await _teamRepository.Create(A.Fake<Team>());
        
        await _projectRepository.Create(new Project()
        {
            Id = 1,
            AuthorId = 1,
            TeamId = 1,
            Tasks = new List<Task>()
            {
                new Task()
                {
                    Id = 1,
                    ProjectId = 1,
                    PerformerId = 1,
                    FinishedAt = DateTime.Now
                    
                },
                new Task()
                {
                    Id = 2,
                    ProjectId = 1,
                    PerformerId = 1,
                    FinishedAt = new DateTime(2022,12,14)
                },
                new Task()
                {
                    Id = 3,
                    ProjectId = 1,
                    PerformerId = 1,
                    FinishedAt = new DateTime(2021,12,14)
                },
                
            }
        });
        
        var b = _linqService.GetRecentlyFinishedTasks(1);
        Assert.Equal(2, (await b).Count);
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Linq3_Incorrect_Input()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _linqService.GetRecentlyFinishedTasks(-1));
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Linq4_TwoTeams_OneOld()
    {
        await _teamRepository.Create(new Team()
        {
            Id = 1,
            Users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    BirthDay = new DateTime(2015, 12, 12)
                },
                new User()
                {
                    Id = 2,
                    BirthDay = new DateTime(1990, 12, 12)
                },
                new User()
                {
                    Id = 3,
                    BirthDay = new DateTime(1990, 12, 12)
                },
            }
        });
        
        await _teamRepository.Create(new Team()
        {
            Id = 2,
            Users = new List<User>()
            {
                new User()
                {
                    Id = 4,
                    BirthDay = new DateTime(1990, 12, 12)
                },
                new User()
                {
                    Id = 5,
                    BirthDay = new DateTime(1990, 12, 12)
                },
                new User()
                {
                    Id = 6,
                    BirthDay = new DateTime(1990, 12, 12)
                },
            }
        });
        
        await _projectRepository.Create(new Project()
        {
            Id = 1,
            AuthorId = 1,
            TeamId = 1,
        });
        await _projectRepository.Create(new Project()
        {
            Id = 2,
            AuthorId = 4,
            TeamId = 2,
        });

        var b = _linqService.GetOldTeams();
        Assert.Single(await b);
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Linq5_ListOfUsersWithTasks()
    {
       
        await _userRepository.Create(new User()
        {
            Id = 1,
            FirstName = "Dima"
        });
        
        await _taskRepository.Create(new Task()
        {
            Id = 1,
            PerformerId = 1,
            Name = "a"
        });
        
        await _taskRepository.Create(new Task()
        {
            Id = 2,
            PerformerId = 1,
            Name = "c"
        });
        
        await _taskRepository.Create(new Task()
        {
            Id = 3,
            PerformerId = 1,
            Name = "b"
        });
        

        var b = _linqService.GetListOfUsersWithTasks();
        Assert.Equal(3, (await b)[0].Tasks.Count);
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Linq6_CorrectUserStructure()
    {
       
        await _userRepository.Create(new User()
        {
            Id = 1,
            FirstName = "Dima",
            TeamId = 1
        });
        
        await _taskRepository.Create(new Task()
        {
            Id = 1,
            PerformerId = 1,
            State = 0
        });
        await _taskRepository.Create(new Task()
        {
            Id = 2,
            PerformerId = 1,
            State = 1
        });
        await _taskRepository.Create(new Task()
        {
            Id = 3,
            PerformerId = 1,
            State = 2
        });

        var b = _linqService.GetUserStructure(1);
        Assert.Equal(2, (await b).NotEndedTasks);
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Linq6_Incorrect_Input()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _linqService.GetUserStructure(-1));
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Linq7_Correct_Proj_DTO()
    {
        await _userRepository.Create(new User()
        {
            Id = 1,
            TeamId = 1
        });
        await _teamRepository.Create(new Team()
        {
            Id = 1,
        });
        await _projectRepository.Create(new Project()
        {
            Id = 1,
            AuthorId = 1,
            TeamId = 1,
            Description = "Proj desc",
            Name = "Proj name"
        });
        await _taskRepository.Create(new Task()
            {
                Id = 1,
                Name = "a",
                Description = "asdasd",
                ProjectId = 1,
                PerformerId = 1
            });
        await _taskRepository.Create(new Task()
        {
            Id = 2,
            Name = "aaasssddd",
            Description = "asd",
            ProjectId = 1,
            PerformerId = 1
        });
        await _taskRepository.Create(new Task()
        {
            Id = 3,
            Name = "qweqweqwe",
            Description = "11",
            ProjectId = 1,
            PerformerId = 1
        });

        var b = _linqService.GetProjectStructure(1);
        Assert.Equal("asdasd", (await b).TaskWithLongestDescription.Description);
    }
    
    [Fact]
    public async System.Threading.Tasks.Task Linq7_Incorrect_Input()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _linqService.GetProjectStructure(-1));
    }

    // [Fact]
    // public void CreateUser_WhenMethodCreateInvokes_ThenCounterIncreses()
    // {
    //     int c = 0;
    //     var fakeUserRepository = new Fake<IRepository<User>>();
    //     fakeUserRepository.CallsTo(userRepository => userRepository.Create(A<User>.Ignored, A<String>.Ignored))
    //         .Invokes(() => c++);
    //     var userCrudService = new UserService(fakeUserRepository.FakedObject);
    //
    //     userCrudService.Create(new User());
    //
    //     Assert.Equal(1, c);
    // }

}