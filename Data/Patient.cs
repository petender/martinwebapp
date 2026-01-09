using System.ComponentModel.DataAnnotations;

namespace martinwebapp.Data;

public class Patient
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
    public DateTime DateOfBirth { get; set; }
    
    [Required]
    [MaxLength(10)]
    public string Gender { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(300)]
    public string Address { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string BloodType { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string Allergies { get; set; } = string.Empty;
    
    [MaxLength(500)]
    public string MedicalHistory { get; set; } = string.Empty;
    
    public DateTime RegistrationDate { get; set; }
}
