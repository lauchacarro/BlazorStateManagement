using BlazorStateManagement.Api.Data;
using BlazorStateManagement.Models;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace BlazorStateManagement.Api.Features
{
    public class GetCustomerByIdRequestHandler(AppDbContext context) : IRequestHandler<GetCustomerByIdRequest, Result<GetCustomerByIdResponse>>
    {
        public async Task<Result<GetCustomerByIdResponse>> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
        {
            var customer = await context.Customers.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (customer is null)
            {
                return "El customer no existe.";
            }

            return new GetCustomerByIdResponse(customer.Id, customer.Name, customer.EmailAddress, customer.Notes);
        }
    }
}
