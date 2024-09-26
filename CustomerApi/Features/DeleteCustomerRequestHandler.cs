using BlazorStateManagement.Api.Data;
using BlazorStateManagement.Models;

using MediatR;

namespace BlazorStateManagement.Api.Features
{
    public class DeleteCustomerRequestHandler(AppDbContext context) : IRequestHandler<DeleteCustomerRequest, Result>
    {
        public async Task<Result> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = await context.Customers.FindAsync(request.Id, cancellationToken);

            if (customer is null)
            {
                return "El customer no existe.";
            }

            context.Customers.Remove(customer);

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}
