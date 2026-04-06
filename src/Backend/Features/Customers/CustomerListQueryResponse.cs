using System;
using System.Xml.Serialization;

namespace Backend.Features.Customers;

[XmlType("Customer")]
public class CustomersListQueryResponse
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    [XmlElement("LastName")]
    public string? LastName { get; set; }

    [XmlElement("Address")]
    public string? Address { get; set; }

    [XmlElement("Email")]
    public string? Email { get; set; }

    [XmlElement("Phone")]
    public string? Phone { get; set; }

    [XmlElement("Iban")]
    public string? Iban { get; set; }

    [XmlElement("Code")]
    public string? Code { get; set; }

    [XmlElement("Description")]
    public string? Description { get; set; }
}

[XmlRoot("Customers")]
public class ExportedCustomersList
{
    [XmlElement("Customer")]
    public List<CustomersListQueryResponse?> Customers { get; set; } = [];
}