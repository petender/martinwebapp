using System.ComponentModel.DataAnnotations;

namespace contosohealth.Data;

public class Medication
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(13)]
    public string EAN { get; set; } = string.Empty;
    
    [Required]
    [Range(0.01, 999999.99)]
    public decimal CostPrice { get; set; }
    
    [Required]
    [Range(0.01, 999999.99)]
    public decimal Dose { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string DoseUnit { get; set; } = string.Empty; // "mg" or "pill"
    
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string Manufacturer { get; set; } = string.Empty;
    
    public DateTime CreatedDate { get; set; }
}
