
public class CustomersListQueryResponse
{

}



public class CustomersListQuery : IRequest<List<CustomersListQueryResponse>>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}


public class CustomersListQueryHandler : IRequestHandler<CustomersListQuery, List<CustomersListQueryResponse>>
{
    private ILogger<CustomersListQueryHandler>? logger;


    public CustomersListQueryHandler(BackendContext context)
    {

    }

    public Task<List<CustomersListQueryResponse>> Handle(CustomersListQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}