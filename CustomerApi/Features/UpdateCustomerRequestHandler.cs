using BlazorStateManagement.Api.Data;
using BlazorStateManagement.Models;

using MediatR;

namespace BlazorStateManagement.Api.Features;

public class UpdateCustomerRequestHandler(AppDbContext context) : IRequestHandler<UpdateCustomerRequest, Result<UpdateCustomerResponse>>
{
    public async Task<Result<UpdateCustomerResponse>> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
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

        //traer el customer por id
        var customer = await context.Customers.FindAsync([request.Id], cancellationToken: cancellationToken);

        if (customer is null)
        {
            return "El customer no existe.";
        }

        //actualizar los campos
        customer.Name = request.Name;
        customer.EmailAddress = request.EmailAddress;
        customer.Notes = request.Notes;

        context.Customers.Update(customer);

        await context.SaveChangesAsync(cancellationToken);

        return new UpdateCustomerResponse(customer.Id, customer.Name, customer.EmailAddress, customer.Notes);

    }
}
