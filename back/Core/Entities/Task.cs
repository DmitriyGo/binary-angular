using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Task : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public int? ProjectId { get; set; }
    public Project? Project { get; set; }
    public int? PerformerId { get; set; }
    public User? Performer { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    [Required]
    [Range(0,3)]
    public int State { get; set; }
    [Column("Created At")]
    public DateTime CreatedAt { get; set; }
    [Column("Finished At")]
    public DateTime? FinishedAt { get; set; }
}