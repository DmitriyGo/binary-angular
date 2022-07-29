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

public class TeamControllerTests
{
    private TeamService _teamService;
    private  TeamRepository _teamRepository;
    private  UserRepository _userRepository;
    private  TeamController _teamController;
    
    public TeamControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Binary")
            .Options;

        var inMemoryDbContext = new ApplicationDbContext(options);
        
        inMemoryDbContext.Database.EnsureDeleted();
        
        _teamRepository = new TeamRepository(inMemoryDbContext);
        
        _teamService = new TeamService(_teamRepository, _userRepository);

        _teamController = new TeamController(_teamService);
    }
    
    [Fact]
    public async System.Threading.Tasks.Task  CreateTeam_WhenTeamIsNull_ReturnBadRequest()
    {
        BadRequestObjectResult result = (BadRequestObjectResult)await _teamController.Post(null);
        
        Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
    }

    [Fact]
    public async System.Threading.Tasks.Task  CreateTeam_WhenTeamIsCreated_AddTeamToDbContext()
    {
        await _teamController.Post(new Team { Id = 1, Name = "asd" });

        Assert.Equal("asd", _teamRepository.GetById(1).Result.Name);
    }
}