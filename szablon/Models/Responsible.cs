using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace szablon.Models;

[PrimaryKey(nameof(BatchId), nameof(EmployeeId))]
public class Responsible
{
    [ForeignKey(nameof(SeedlingBatch))]
    public int BatchId { get; set; }
    [ForeignKey(nameof(Employee))]
    public int EmployeeId { get; set; }
    [MaxLength(100)]
    public string Role { get; set; }
    
    public SeedlingBatch SeedlingBatch { get; set; }
    public Employee Employee { get; set; }
}