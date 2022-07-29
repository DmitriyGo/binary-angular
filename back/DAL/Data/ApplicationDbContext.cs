using Bogus;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Task = Core.Entities.Task;

namespace DAL.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        var ids1 = 1;
        var projects = new Faker<Project>()
             .RuleFor(x => x.Id, f => ids1)
             .RuleFor(x => x.AuthorId, x => ids1++)
             .RuleFor(x => x.TeamId, x => x.Random.Int(1, 10))
             .RuleFor(x => x.Name, x => x.Company.CompanyName())
             .RuleFor(x => x.Description, x => x.Random.String2(60))
             .RuleFor(x => x.CreatedAt, x => x.Date.Past())
             .RuleFor(x => x.DeadLine, x => x.Date.Future());

        var ids2 = 1;
        var teams = new Faker<Team>()
            .RuleFor(x => x.Id, f => ids2++)
            .RuleFor(x => x.Name, x => x.Company.CompanyName())
            .RuleFor(x => x.CreatedAt, x => x.Date.Past());


        var ids3 = 1;
        var tasks = new Faker<Task>()
            .RuleFor(x => x.Id, f => ids3++)
            .RuleFor(x => x.Name, x => x.Random.String2(60))
            .RuleFor(x => x.Description, x => x.Random.String2(60))
            .RuleFor(x => x.PerformerId, x => x.Random.Int(1,100))
            .RuleFor(x => x.ProjectId, x => x.Random.Int(1, 100))
            .RuleFor(x => x.CreatedAt, x => x.Date.Past())
            .RuleFor(x => x.FinishedAt, x => x.Date.Future())
            .RuleFor(x => x.State, x => x.Random.Int(0, 3));

        var ids4 = 1;
        var users = new Faker<User>()
            .RuleFor(x => x.Id, f => ids4++)
                .RuleFor(x => x.Email, x => x.Person.Email)
                .RuleFor(x => x.BirthDay, x => x.Date.Past())
                .RuleFor(x => x.FirstName, x => x.Person.FirstName)
                .RuleFor(x => x.LastName, x => x.Person.LastName)
                .RuleFor(x => x.RegisteredAt, x => x.Date.Past())
                .RuleFor(x => x.TeamId, f => f.Random.Int(1, 10));




        modelBuilder.Entity<Project>().HasData(projects.Generate(100));
        modelBuilder.Entity<Task>().HasData(tasks.Generate(200));
        modelBuilder.Entity<User>().HasData(users.Generate(100));
        modelBuilder.Entity<Team>().HasData(teams.Generate(10));
    }
    


    public DbSet<Team> Teams { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Project> Projects { get; set; }

}