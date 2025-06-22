using Microsoft.EntityFrameworkCore;
using szablon.Models;

namespace szablon.Data;

public class DatabaseContext : DbContext
{
    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Nursery> Nurseries { get; set; }
    public DbSet<SeedlingBatch> SeedlingBatches { get; set; }
    public DbSet<TreeSpecies> TreeSpecies { get; set; }
    public DbSet<Responsible> Responsibles { get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasData(
            new Employee { EmployeeId = 1, FirstName = "Ewa", LastName = "Wiśniewska", HireDate = new DateTime(2018, 8, 12) },
            new Employee { EmployeeId = 2, FirstName = "Marek", LastName = "Lewandowski", HireDate = new DateTime(2021, 1, 20) },
            new Employee { EmployeeId = 3, FirstName = "Katarzyna", LastName = "Zielińska", HireDate = new DateTime(2017, 11, 5) }
        );

        modelBuilder.Entity<Nursery>().HasData(
            new Nursery { NurseryId = 1, Name = "Zielona Szkółka", EstablishmentDate = new DateTime(2005, 5, 10) },
            new Nursery { NurseryId = 2, Name = "Szkółka Leśna", EstablishmentDate = new DateTime(2012, 9, 15) }
        );

        modelBuilder.Entity<TreeSpecies>().HasData(
            new TreeSpecies { SpeciesId = 1, LatinName = "Quercus robur", GrowthTimeInYears = 5 },
            new TreeSpecies { SpeciesId = 2, LatinName = "Fagus sylvation", GrowthTimeInYears = 7 },
            new TreeSpecies { SpeciesId = 3, LatinName = "Abies alba", GrowthTimeInYears = 6 }
        );

        modelBuilder.Entity<SeedlingBatch>().HasData(
            new SeedlingBatch
            {
                BatchId = 1,
                NurseryId = 1,
                SpeciesId = 1,
                Quantity = 500,
                SownDate = new DateTime(2024, 3, 15),
                ReadyDate = new DateTime(2029, 3, 15)
            },
            new SeedlingBatch
            {
                BatchId = 2,
                NurseryId = 2,
                SpeciesId = 3,
                Quantity = 300,
                SownDate = new DateTime(2023, 6, 10),
                ReadyDate = null
            }
        );

        modelBuilder.Entity<Responsible>().HasData(
            new Responsible
            {
                BatchId = 1,
                EmployeeId = 1,
                Role = "Supervisor"
            },
            new Responsible
            {
                BatchId = 1,
                EmployeeId = 3,
                Role = "Planter"
            },
            new Responsible
            {
                BatchId = 2,
                EmployeeId = 2,
                Role = "Assistant"
            }
        );

    }
}