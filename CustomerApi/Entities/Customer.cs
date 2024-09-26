namespace BlazorStateManagement.Api.Entities;

public class Customer(string name, string emailAddress, string notes)
{
    public Guid Id { get; set; }
    public string Name { get; set; } = name;
    public string EmailAddress { get; set; } = emailAddress;
    public string Notes { get; set; } = notes;
}
