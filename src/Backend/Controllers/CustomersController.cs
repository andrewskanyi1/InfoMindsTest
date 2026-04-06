using Backend.Features.Customers;
using Microsoft.AspNetCore.Mvc;
namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(ILogger<CustomersController> logger, IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;
    private ILogger<CustomersController> logger = logger;

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