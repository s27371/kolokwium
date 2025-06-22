using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using szablon.Data;
using szablon.DTOs;
using szablon.Models;

namespace szablon.Services;

public class DbService(DatabaseContext context) : IDbService
{
    public async Task<NurseryWithBatchesDto> GetNurseryWithBatchesAsync(int nurseryId)
    {
        var nursery = await context.Nurseries
            .Where(n => n.NurseryId == nurseryId)
            .Select(n => new NurseryWithBatchesDto
            {
                NurseryId = n.NurseryId,
                Name = n.Name,
                EstablishedDate = n.EstablishmentDate,
                Batches = n.SeedlingBatches.Select(b => new BatchDto
                {
                    BatchId = b.BatchId,
                    Quantity = b.Quantity,
                    SownDate = b.SownDate,
                    ReadyDate = b.ReadyDate,
                    Species = new SpeciesDto
                    {
                        LatinName = b.TreeSpecies.LatinName,
                        GrowthTimeInYears = b.TreeSpecies.GrowthTimeInYears
                    },
                    Responsible = b.Responsibles.Select(r => new ResponsibleDto
                    {
                        FirstName = r.Employee.FirstName,
                        LastName = r.Employee.LastName,
                        Role = r.Role
                    }).ToList()
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (nursery == null) {
            throw new KeyNotFoundException($"Nursery with ID {nurseryId} not found");
        }

        return nursery;
    }

    public async Task AddSeedlingBatchAsync(BatchRequestDto dto)
    {
        var nursery = await context.Nurseries.FirstOrDefaultAsync(n => n.Name == dto.Nursery);
        if (nursery == null)
        {
            throw new Exception($"Nursery with ID {dto.Nursery} not found");
        }

        var species = await context.TreeSpecies.FirstOrDefaultAsync(s => s.LatinName == dto.Species);
        if (species == null)
        {
            throw new Exception("Species not found");
        }

        var invalidEmployee = dto.Responsible.FirstOrDefault(r => !context.Employees.Any(e => e.EmployeeId == r.EmployeeId));
        if (invalidEmployee != null)
        {
            throw new Exception($"Employee with ID {invalidEmployee.EmployeeId} not found");
        }

        var newBatch = new SeedlingBatch
        {
            NurseryId = nursery.NurseryId,
            SpeciesId = species.SpeciesId,
            Quantity = dto.Quantity,
            SownDate = DateTime.UtcNow,
            ReadyDate = DateTime.UtcNow.AddYears(species.GrowthTimeInYears),
        };

        context.SeedlingBatches.Add(newBatch);
        await context.SaveChangesAsync(); 

        foreach (var responsible in dto.Responsible)
        {
            context.Responsibles.Add(new Responsible
            {
                BatchId = newBatch.BatchId,
                EmployeeId = responsible.EmployeeId,
                Role = responsible.Role
            });
        }

        await context.SaveChangesAsync();
    }
}