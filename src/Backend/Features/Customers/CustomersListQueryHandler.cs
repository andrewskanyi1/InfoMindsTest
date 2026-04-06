using Backend.Features.Customers;

public class CustomersListQueryHandler : IRequestHandler<CustomersListQuery, List<CustomersListQueryResponse>>
{
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

            IQueryable<Customer> queryResult = context.Customers;

            if (!string.IsNullOrEmpty(request.SearchText))
            {
                queryResult = queryResult.Where(customer => customer.Name.Contains(request.SearchText)
                || customer.Email.Contains(request.SearchText)
                );
            }

            //Add optional ordering
            if (!string.IsNullOrEmpty(request.SortColumn))
            {
                //Add filtering by email
                if (request.SortColumn.Equals("Email", StringComparison.OrdinalIgnoreCase))
                {
                    queryResult = queryResult.OrderBy(customer => customer.Email);
                }
                else if (request.SortColumn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    queryResult = queryResult.OrderBy(customer => customer.Name);
                }

                // Add more optional filtering here
            }

            result = [..queryResult.Select(g => new CustomersListQueryResponse()
            {
                FirstName = g.Name,
                Iban = g.Iban,
                Code = g.CustomerCategory.Code ?? string.Empty,
                Description = g.CustomerCategory.Description ?? string.Empty,
                Address = g.Address,
                Email = g.Email,
                Phone = g.Phone,
                Id = g.Id
            }).Skip(request.Skip).Take(request.Take)];
        }
        catch (Exception e)
        {

        }

        return result;
    }
}