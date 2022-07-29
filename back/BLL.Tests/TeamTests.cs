using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using DAL.Data;
using DAL.Interfaces;
using DAL.Repositories;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Xunit;

namespace BLL.Tests;

public class TeamServiceTest
{
    private TeamService _teamService;
    
    private  TeamRepository _teamRepository;
    private  UserRepository _userRepository;
    
    public TeamServiceTest()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Binary")
            .Options;

        var inMemoryDbContext = new ApplicationDbContext(options);
        
        inMemoryDbContext.Database.EnsureDeleted();
        
        _teamRepository = new TeamRepository(inMemoryDbContext);
        _userRepository = new UserRepository(inMemoryDbContext);
        
        
        _teamService = new TeamService(_teamRepository, _userRepository);
    }
    
    
    [Fact]
    public async System.Threading.Tasks.Task AddUserToTeam_WhenMethodCreateInvokes_ThenCounterIncreses()
    {
        var team = new Team
        {
            Id = 1
        };
        var user = new User
        {
            Id = 1
        };

        await _userRepository.Create(user);
        await _teamService.Create(team);
      
        await _teamService.AddUserToTeam(1, 1);

        Assert.Equal(1, (await _teamService.GetById(1)).Users.Count);
    } 
    
    [Fact]
    public async System.Threading.Tasks.Task AddUserToTeam_WhenUserOrTeamDoesNotExist_ThrowNullReferenceException()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _teamService.AddUserToTeam(1, 1));
    }
}