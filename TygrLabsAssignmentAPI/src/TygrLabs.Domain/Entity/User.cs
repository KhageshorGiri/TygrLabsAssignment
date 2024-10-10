using System.ComponentModel.DataAnnotations;

namespace TygrLabs.Domain.Entity;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(150, MinimumLength = 4)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(150, MinimumLength = 4)]
    public string LastName { get; set; }
}
