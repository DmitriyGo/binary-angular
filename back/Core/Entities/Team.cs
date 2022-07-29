using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Abstract;

namespace Core.Entities;

public class Team : BaseEntity
{
    
    public int Id { get; set; }
    [MaxLength(50)]
    public string? Name { get; set; }
    [Column("Created At")]
    public DateTime CreatedAt { get; set; }
    public ICollection<User> Users { get; set; }
    public ICollection<Project> Projects { get; set; }
}