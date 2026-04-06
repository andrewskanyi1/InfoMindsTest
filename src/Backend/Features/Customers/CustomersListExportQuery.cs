namespace Backend.Features.Customers;

public class CustomersListExportQuery : IRequest<byte[]>
{
    public int Id { get; set; }
    public string? FirstName { get; set; }

    public string? SearchText { get; set; }
}

