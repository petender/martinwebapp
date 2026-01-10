using System.ComponentModel.DataAnnotations;

namespace martinwebapp.Data;

public class Doctor
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(150)]
    public string Specialization { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string LicenseNumber { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;
    
    [MaxLength(200)]
    public string Department { get; set; } = string.Empty;
    
    public int YearsOfExperience { get; set; }
    
    [MaxLength(500)]
    public string Education { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string Bio { get; set; } = string.Empty;
    
    [MaxLength(300)]
    public string PhotoUrl { get; set; } = string.Empty;
    
    public DateTime JoinDate { get; set; }
}
