using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DataUpdateController : ControllerBase
{
    private readonly ApiDataService _apiDataService;

    public DataUpdateController(ApiDataService apiDataService)
    {
        _apiDataService = apiDataService;
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateData()
    {
        try
        {
            await _apiDataService.UpdateAllTablesAsync();
            return Ok("Data updated successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message} - {ex.StackTrace}");
        }
    }
}