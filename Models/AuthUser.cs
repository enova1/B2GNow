using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_CORE.Models;

/// <summary>
/// Authorized User model for the AuthUser view.
/// </summary>
public class AuthUser
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Column(TypeName = "nvarchar(100)")]
    [Required]
    public string? FirstName { get; set; }
    [Column(TypeName = "nvarchar(100)")]
    [Required]
    public string? LastName { get; set; }
    public string? Password { get; set; }
    public DateTime RegistrationDate{ get; set; }
    public string? PhoneNumber { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    [Required]
    public string? PostCode { get; set; }
}