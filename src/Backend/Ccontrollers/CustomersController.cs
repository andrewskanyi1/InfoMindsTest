using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]

    public async Task<IActionResult> GetCustomersList()
    {

        var customersListQuery = new CustomersListQuery();
        var result = await mediator.Send(customersListQuery);

        return Ok(result);
    }
}