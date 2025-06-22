using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using szablon.DTOs;
using szablon.Services;

namespace szablon.Controllers
{
    [Route("api/")]
    [ApiController]
    public class BatchesController(IDbService service) : ControllerBase
    {
        [HttpGet("nurseries/{id}/batches")]
        public async Task<IActionResult> GetBatches(int id)
        {
            try
            {
                var result = await service.GetNurseryWithBatchesAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("batches")]
        public async Task<IActionResult> AddBatch([FromBody] BatchRequestDto dto)
        {
            try
            {
                await service.AddSeedlingBatchAsync(dto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
