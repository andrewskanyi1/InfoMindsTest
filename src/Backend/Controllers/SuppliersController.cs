using Backend.Features.Suppliers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SuppliersController : ControllerBase
{
    private IMediator mediator;

    public SuppliersController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("list")]
    public IActionResult GetSuppliersList([FromQuery]SupplierListQuery query)
    {
        var result = mediator.Send(query);
        return Ok(result);
    }
}

