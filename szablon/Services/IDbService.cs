using szablon.DTOs;

namespace szablon.Services;

public interface IDbService
{
    Task<NurseryWithBatchesDto> GetNurseryWithBatchesAsync(int nurseryId);
    Task AddSeedlingBatchAsync(BatchRequestDto dto);

}