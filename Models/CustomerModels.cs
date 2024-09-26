using MediatR;

namespace BlazorStateManagement.Models
{
    public record CreateCustomerRequest(string Name, string EmailAddress, string Notes) : IRequest<Result<CreateCustomerResponse>>;
    public record CreateCustomerResponse(Guid Id, string Name, string EmailAddress, string Notes);

    public record UpdateCustomerRequest(Guid Id, string Name, string EmailAddress, string Notes) : IRequest<Result<UpdateCustomerResponse>>;
    public record UpdateCustomerResponse(Guid Id, string Name, string EmailAddress, string Notes);

    public record GetCustomerByIdRequest(Guid Id) : IRequest<Result<GetCustomerByIdResponse>>;
    public record GetCustomerByIdResponse(Guid Id, string Name, string EmailAddress, string Notes);

    public record CustomersItem(Guid Id, string Name, string EmailAddress, string Notes);
    public record SearchCustomersRequest(string? Search) : IRequest<Result<SearchCustomersResponse>>;
    public record SearchCustomersResponse(IEnumerable<CustomersItem> Customers);

    public record DeleteCustomerRequest(Guid Id) : IRequest<Result>;

}
