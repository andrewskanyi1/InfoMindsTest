using System;

namespace Backend.Features.Customers;

public class CustomersListQuery : IRequest<CustomersListQueryPagedResponse>
{
    public int Id { get; set; }
    public string? FirstName { get; set; }

    public string? SearchText { get; set; }

    public int Skip { get; set; }
    public int Take { get; set; }
    public string? SortColumn { get; set; }
}

public class CustomersListQueryPagedResponse
{
    public List<CustomersListQueryResponse> Items { get; set; } = [];
    public int TotalCount { get; set; }
}