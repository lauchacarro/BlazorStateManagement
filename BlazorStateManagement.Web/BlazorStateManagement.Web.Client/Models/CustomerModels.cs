using BlazorStateManagement.Models;

namespace BlazorStateManagement.Web.Client.Models;


public class CreateCustomerFormModel
{
    public string? Name { get; set; }
    public string? EmailAddress { get; set; }
    public string? Notes { get; set; }

    // de forma implicita que se convierta a CreateCustomerRequest
    public static implicit operator CreateCustomerRequest(CreateCustomerFormModel model)
    {
        return new CreateCustomerRequest(model.Name!, model.EmailAddress!, model.Notes!);
    }
}

public class UpdateCustomerFormModel
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? EmailAddress { get; set; }
    public string? Notes { get; set; }

    public static implicit operator UpdateCustomerRequest(UpdateCustomerFormModel model)
    {
        return new UpdateCustomerRequest(model.Id, model.Name!, model.EmailAddress!, model.Notes!);
    }
}