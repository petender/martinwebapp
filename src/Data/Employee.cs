using System.ComponentModel.DataAnnotations;

namespace contosohealth.Data;

public class Employee
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
    public DateTime HiringDate { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Department { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(150)]
    public string JobTitle { get; set; } = string.Empty;
}
