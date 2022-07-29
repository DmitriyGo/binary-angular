using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class User : BaseEntity
{
    public int Id { get; set; }
    public int? TeamId { get; set; }
    //public Team Team { get; set; }
    [MaxLength(60)]
    [Column("First Name")]
    public string? FirstName { get; set; }
    [MaxLength(60)]
    [Column("Last Name")]
    public string? LastName { get; set; }
    [MaxLength(60)]
    public string? Email { get; set; }
    [Column("Registered At")]
    public DateTime RegisteredAt { get; set; }
    public DateTime BirthDay { get; set; }
}