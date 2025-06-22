using System.ComponentModel.DataAnnotations;

namespace szablon.Models;

public class Nursery
{
    [Key]
    public int NurseryId { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    public DateTime EstablishmentDate { get; set; }
    
    public ICollection<SeedlingBatch> SeedlingBatches { get; set; } 
}