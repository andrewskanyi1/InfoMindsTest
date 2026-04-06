using System;
using System.Linq;
using System.Xml.Serialization;

namespace Backend.Features.Customers.Queries;

public class CustomersListExportQueryHandler : IRequestHandler<CustomersListExportQuery, byte[]>
{
    private BackendContext context;

    public CustomersListExportQueryHandler(BackendContext context)
    {
        this.context = context;

    }
    public async Task<byte[]> Handle(CustomersListExportQuery request, CancellationToken cancellationToken)
    {
        byte[] result = [];
        try
        {
            IQueryable<Customer> queryResult = context.Customers;

            if (!string.IsNullOrEmpty(request.SearchText))
            {
                queryResult = queryResult.Where(customer => customer.Name.Contains(request.SearchText)
            || customer.Email.Contains(request.SearchText)
                );
            }

            List<CustomersListQueryResponse> customers = await queryResult.Select(g => new CustomersListQueryResponse()
            {
                FirstName = g.Name,
                Iban = g.Iban,
                Code = g.CustomerCategory.Code ?? string.Empty,
                Description = g.CustomerCategory.Description ?? string.Empty,
                Address = g.Address,
                Email = g.Email,
                Phone = g.Phone,
                Id = g.Id
            }).ToListAsync(cancellationToken);

            var exportedList = new ExportedCustomersList()
            {
                Customers = customers
            };

            var serializer = new XmlSerializer(typeof(ExportedCustomersList));
            using var memoryStream = new MemoryStream();
            serializer.Serialize(memoryStream, exportedList);
            result = memoryStream.ToArray();
        }
        catch (Exception e)
        {
            throw;

        }


        return result;
    }
}
