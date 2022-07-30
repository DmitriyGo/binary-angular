using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities.Abstract;

namespace Core.Entities;

public class Project : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public int? AuthorId { get; set; }
    public User? Author { get; set; }
    public int? TeamId { get; set; }
    public Team? Team { get; set; }
    [MaxLength(60)]
    public string? Name { get; set; }
    [MaxLength(400)]
    public string? Description { get; set; }
    public DateTime DeadLine { get; set; }
    [Required]
    [Column("Created At")]
    public DateTime CreatedAt { get; set; }
    public ICollection<Task>? Tasks { get; set; }
}