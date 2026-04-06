using System.Xml.Serialization;

namespace Backend.Features.Customers;

[XmlRoot("Customers")]
public class ExportedCustomersList
{
    [XmlElement("Customer")]
    public List<CustomersListQueryResponse> Customers { get; set; } = [];
}