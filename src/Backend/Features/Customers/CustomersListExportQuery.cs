namespace Backend.Features.Customers;

public class CustomersListExportQuery : IRequest<byte[]>
{
    public int Id { get; set; }
    public string? FirstName { get; set; }

    public string? SearchText { get; set; }

    // public int Skip { get; set; }
    // public int Take { get; set; }
    // public string? SortColumn { get; set; }
}

