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

public class ProjectControllerTests
{
    private ProjectService _projectService;
    private  ProjectRepository _projectRepository;
    private  ProjectController _projectController;
    
    public ProjectControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Binary")
            .Options;

        var inMemoryDbContext = new ApplicationDbContext(options);
        
        inMemoryDbContext.Database.EnsureDeleted();
        
        _projectRepository = new ProjectRepository(inMemoryDbContext);
        
        _projectService = new ProjectService(_projectRepository);

        _projectController = new ProjectController(_projectService);
    }
    
    [Fact]
    public async System.Threading.Tasks.Task  CreateProject_WhenProjectIsNull_ReturnBadRequest()
    {
        BadRequestObjectResult result = (BadRequestObjectResult)await _projectController.Post(null);
        
        Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
    }

    [Fact]
    public async System.Threading.Tasks.Task  CreateProject_WhenProjectIsCreated_AddProjectToDbContext()
    {
        await _projectController.Post(new Project { Id = 1, Name = "asd" });

        Assert.Equal("asd", _projectRepository.GetById(1).Result.Name);
    }
}