using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/pricing")]
public class PricingController : ControllerBase
{
    private readonly PricingService _pricingService;

    public PricingController(PricingService pricingService)
    {
        _pricingService = pricingService;
    }

    [HttpGet("calculate")]
    public IActionResult Calculate([FromQuery] OrderRequest request)
    {
        try
        {
            return Ok(_pricingService.CalculatePrice(request));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
