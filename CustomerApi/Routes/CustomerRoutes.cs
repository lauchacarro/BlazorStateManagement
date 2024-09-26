using BlazorStateManagement.Api.Extensions;
using BlazorStateManagement.Models;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BlazorStateManagement.Api.Routes
{
    public static class CustomerRoutes
    {
        const string PATH = "Customers";

        public static IEndpointRouteBuilder MapCustomers(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup(PATH);

            group.MapGet("", ([FromQuery] string? search, [FromServices] IMediator mediator)
                => mediator.Send(new SearchCustomersRequest(search)).ToHttpResult());

            group.MapGet("{id:guid}", (Guid id, [FromServices] IMediator mediator)
                => mediator.Send(new GetCustomerByIdRequest(id)).ToHttpResult());

            group.MapPost("", (CreateCustomerRequest request, [FromServices] IMediator mediator)
                => mediator.Send(request).ToHttpResult());

            group.MapPut("", (UpdateCustomerRequest request, [FromServices] IMediator mediator)
                => mediator.Send(request).ToHttpResult());

            group.MapDelete("{id:guid}", (Guid id, [FromServices] IMediator mediator)
                => mediator.Send(new DeleteCustomerRequest(id)).ToHttpResult());

            return group;

        }
    }
}
