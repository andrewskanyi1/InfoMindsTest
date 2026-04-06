using Microsoft.AspNetCore.Mvc;
namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private IMediator mediator;
    private ILogger<CustomersController> logger;

    public CustomersController(ILogger<CustomersController> logger, IMediator mediator)
    {
        this.mediator = mediator;
        this.logger = logger;
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetCustomersList([FromQuery] CustomersListQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("export")]
    public async Task<IActionResult> ExportCustomersList([FromQuery] CustomersListQuery query)
    {
        var result = await mediator.Send(query);

        return Ok(result);
    }
}