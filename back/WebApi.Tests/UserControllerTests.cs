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

public class UserControllerTests
{
    private UserService _userService;
    private  UserRepository _userRepository;
    private  UserController _userController;
    
    public UserControllerTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Binary")
            .Options;

        var inMemoryDbContext = new ApplicationDbContext(options);
        
        inMemoryDbContext.Database.EnsureDeleted();
        
        _userRepository = new UserRepository(inMemoryDbContext);
        
        _userService = new UserService(_userRepository);

        _userController = new UserController(_userService);
    }
    
    [Fact]
    public async System.Threading.Tasks.Task  DeleteUser_WhenUserDoesntExists_ReturnBadRequest()
    {
        BadRequestObjectResult result = (BadRequestObjectResult)await _userController.Delete(2);
        
        Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
    }

    [Fact]
    public async System.Threading.Tasks.Task  DeleteUser_WhenUserIsDeleted_ThenStatus200()
    {
        await _userController.Post(new User {Id = 1, FirstName = "Dimas"});
        
        var result = (OkResult)await _userController.Delete(1);
        
        Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
    }
}