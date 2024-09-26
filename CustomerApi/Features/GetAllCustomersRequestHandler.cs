using BlazorStateManagement.Api.Data;
using BlazorStateManagement.Models;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace BlazorStateManagement.Api.Features
{
    public class GetAllCustomersRequestHandler(AppDbContext context) : IRequestHandler<SearchCustomersRequest, Result<SearchCustomersResponse>>
    {
        public async Task<Result<SearchCustomersResponse>> Handle(SearchCustomersRequest request, CancellationToken cancellationToken)
        {
            var query = context.Customers.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x => x.Name.Contains(request.Search));
            }

            var customers = await query
                .Select(x => new CustomersItem(x.Id, x.Name, x.EmailAddress, x.Notes))
                .ToListAsync(cancellationToken);

            return new SearchCustomersResponse(customers);
        }
    }

}
