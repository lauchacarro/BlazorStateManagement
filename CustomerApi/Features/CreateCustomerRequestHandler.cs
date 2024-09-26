using BlazorStateManagement.Api.Data;
using BlazorStateManagement.Api.Entities;
using BlazorStateManagement.Models;

using MediatR;

namespace BlazorStateManagement.Api.Features;

public class CreateCustomerRequestHandler(AppDbContext context) : IRequestHandler<CreateCustomerRequest, Result<CreateCustomerResponse>>
{
    public async Task<Result<CreateCustomerResponse>> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        //validar que los campos no esten vacios
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return "El nombre es requerido.";
        }

        if (string.IsNullOrWhiteSpace(request.EmailAddress))
        {
            return "El email es invalido.";
        }

        if (string.IsNullOrWhiteSpace(request.Notes))
        {
            return "Las notas son requeridas.";
        }


        var customer = new Customer(request.Name, request.EmailAddress, request.Notes);

        context.Customers.Add(customer);

        await context.SaveChangesAsync(cancellationToken);

        return new CreateCustomerResponse(customer.Id, customer.Name, customer.EmailAddress, customer.Notes);

    }
}
