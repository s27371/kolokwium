using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace szablon.Models;

[Table("Tree_Species")]
public class TreeSpecies
{
    [Key]
    public int SpeciesId { get; set; }
    [MaxLength(100)]
    public string LatinName { get; set; }
    public int GrowthTimeInYears { get; set; }

    public ICollection<SeedlingBatch> SeedlingBatches { get; set; }
}