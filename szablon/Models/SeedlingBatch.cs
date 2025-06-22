using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace szablon.Models;

[Table("Seedling_Batch")]
public class SeedlingBatch
{
    [Key]
    public int BatchId { get; set; }
    [ForeignKey(nameof(Nursery))]
    public int NurseryId { get; set; }
    [ForeignKey(nameof(TreeSpecies))]
    public int SpeciesId { get; set; }
    public int Quantity { get; set; }
    public DateTime SownDate { get; set; }
    public DateTime? ReadyDate { get; set; }
    
    public Nursery Nursery { get; set; }
    public TreeSpecies TreeSpecies { get; set; }
    
    public ICollection<Responsible> Responsibles { get; set; }
}