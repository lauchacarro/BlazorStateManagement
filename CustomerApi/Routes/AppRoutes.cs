using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace BlazorStateManagement.Api.Routes
{
    public static class AppRoutes
    {
        public static IEndpointRouteBuilder MapAppApi(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("api");

            group.MapCustomers();

            return group;
        }
    }
}
