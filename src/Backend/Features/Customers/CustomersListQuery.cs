
public class CustomersListQueryResponse
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Iban { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }


}



public class CustomersListQuery : IRequest<List<CustomersListQueryResponse>>
{
    public int Id { get; set; }
    public string? FirstName { get; set; }

}


public class CustomersListQueryHandler : IRequestHandler<CustomersListQuery, List<CustomersListQueryResponse>>
{
    private ILogger<CustomersListQueryHandler>? logger;
    private readonly BackendContext context;

    public CustomersListQueryHandler(BackendContext context)
    {
        this.context = context;
    }

    public async Task<List<CustomersListQueryResponse>> Handle(CustomersListQuery request, CancellationToken cancellationToken)
    {
        var result = new List<CustomersListQueryResponse>();
        try
        {
            var queryResult = context.Customers.Select(g => new CustomersListQueryResponse()
            {
                FirstName = g.Name,
                Iban = g.Iban,
                Code = g.CustomerCategory.Code ?? string.Empty,
                Description = g.CustomerCategory.Description ?? string.Empty,
                Address = g.Address,
                Email = g.Email,
                Phone = g.Phone,
                Id = g.Id
            }).ToList();
            result = queryResult;

        }
        catch (Exception e)
        {

        }

        return result;
    }
}