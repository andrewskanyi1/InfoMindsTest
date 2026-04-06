
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Features.Employees;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class EmployeesController : ControllerBase
{
    private IMediator mediator;

    public EmployeesController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    [HttpGet("list")]
    public async Task<IActionResult> GetEmployeesList([FromQuery] EmployeesListQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }
}
