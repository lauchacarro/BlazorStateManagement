using System.Collections.Immutable;
using System.Net.Http.Json;

using BlazorStateManagement.Models;
using BlazorStateManagement.Store.Extensions;

using Fluxor;

using Microsoft.AspNetCore.Components;

namespace BlazorStateManagement.Store
{
    [FeatureState]
    public record CustomersState(
        ImmutableArray<CustomersItem> Customers,
        bool IsLoading,
        ImmutableArray<string> Errors)
    {

        public static readonly CustomersState Empty = new();

        private CustomersState() :
            this(
                Customers: [],
                IsLoading: false,
                Errors: [])
        {
        }
    }


    public record CreateCustomerAction(CreateCustomerRequest Request);
    public record CreateCustomerSuccessAction(CreateCustomerResponse Response);

    public record UpdateCustomerAction(UpdateCustomerRequest Request);
    public record UpdateCustomerSuccessAction(UpdateCustomerResponse Response);

    public record GetCustomerByIdAction(GetCustomerByIdRequest Request);
    public record GetCustomerByIdSuccessAction(GetCustomerByIdResponse Response);

    public record SearchCustomersAction(SearchCustomersRequest Request);
    public record SearchCustomersSuccessAction(SearchCustomersResponse Response);

    public record DeleteCustomerAction(DeleteCustomerRequest Request);
    public record DeleteCustomerSuccessAction(Guid Id);

    public record CustomerErrorAction(IEnumerable<string> Errors);
    public record CustomerLoadingAction(bool IsLoading);


    public class CustomersEffects(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;


        [EffectMethod]
        public async Task HandleCreateCustomerAction(CreateCustomerAction action, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new CustomerLoadingAction(true));

            var response = await _httpClient.PostAsJsonAsync("/api/customers", action.Request);
            var result = await response.Content.ReadFromJsonAsync<Result<CreateCustomerResponse>>();

            if (result != null && result.Succeeded)
            {
                dispatcher.Dispatch(new CreateCustomerSuccessAction(result.Data!));
            }
            else
            {
                dispatcher.Dispatch(new CustomerErrorAction(result!.Errors));
            }
        }

        [EffectMethod]
        public async Task HandleUpdateCustomerAction(UpdateCustomerAction action, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new CustomerLoadingAction(true));

            var response = await _httpClient.PutAsJsonAsync($"/api/customers", action.Request);
            var result = await response.Content.ReadFromJsonAsync<Result<UpdateCustomerResponse>>();
            if (result != null && result.Succeeded)
            {
                dispatcher.Dispatch(new UpdateCustomerSuccessAction(result.Data!));
            }
            else
            {
                dispatcher.Dispatch(new CustomerErrorAction(result!.Errors));
            }
        }

        [EffectMethod]
        public async Task HandleGetCustomerByIdAction(GetCustomerByIdAction action, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new CustomerLoadingAction(true));

            var response = await _httpClient.GetAsync($"/api/customers/{action.Request.Id}");
            var result = await response.Content.ReadFromJsonAsync<Result<GetCustomerByIdResponse>>();

            if (result != null && result.Succeeded)
            {
                dispatcher.Dispatch(new GetCustomerByIdSuccessAction(result.Data!));
            }
            else
            {
                dispatcher.Dispatch(new CustomerErrorAction(result!.Errors));
            }
        }

        [EffectMethod]
        public async Task HandleSearchCustomersAction(SearchCustomersAction action, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new CustomerLoadingAction(true));

            var response = await _httpClient.GetAsync($"/api/customers?search={action.Request.Search}");
            var result = await response.Content.ReadFromJsonAsync<Result<SearchCustomersResponse>>();

            if (result != null && result.Succeeded)
            {
                dispatcher.Dispatch(new SearchCustomersSuccessAction(result.Data!));
            }
            else
            {
                dispatcher.Dispatch(new CustomerErrorAction(result!.Errors));
            }
        }

        [EffectMethod]
        public async Task HandleDeleteCustomerAction(DeleteCustomerAction action, IDispatcher dispatcher)
        {

            var response = await _httpClient.DeleteAsync($"/api/customers/{action.Request.Id}");
            var result = await response.Content.ReadFromJsonAsync<Result>();

            if (result != null && result.Succeeded)
            {
                dispatcher.Dispatch(new DeleteCustomerSuccessAction(action.Request.Id));
            }
            else
            {
                dispatcher.Dispatch(new CustomerErrorAction(result!.Errors));
            }
        }
    }







    public static class CustomersReducers
    {
        [ReducerMethod]
        public static CustomersState OnCreateCustomerSuccess(CustomersState state, CreateCustomerSuccessAction action)
            => state with
            {
                Customers = state.Customers.Add(new CustomersItem(action.Response.Id, action.Response.Name, action.Response.EmailAddress, action.Response.Notes)),
            };




        [ReducerMethod]
        public static CustomersState OnSearchCustomersSuccess(CustomersState state, SearchCustomersSuccessAction action)
            => new CustomersState(action.Response.Customers.ToImmutableArray(), false, []);


        [ReducerMethod]
        public static CustomersState OnGetCustomerByIdSuccess(CustomersState state, GetCustomerByIdSuccessAction action)
          => !state.Customers.ReplaceOne(
                  selector: x => x.Id == action.Response.Id,
                  replacement: x => x with { Name = x.Name, EmailAddress = x.EmailAddress, Notes = x.Notes },
                  result: out var newCustomers)
              ? state
              : state with { Customers = newCustomers, IsLoading = false };



        [ReducerMethod]
        public static CustomersState OnUpdateCustomerSuccess(CustomersState state, UpdateCustomerSuccessAction action)
          => !state.Customers.ReplaceOne(
                  selector: x => x.Id == action.Response.Id,
                  replacement: x => x with { Name = x.Name, EmailAddress = x.EmailAddress, Notes = x.Notes },
                  result: out var newCustomers)
              ? state
              : state with
              {
                  Customers = newCustomers,
              };




        [ReducerMethod]
        public static CustomersState OnDeleteCustomerSuccess(CustomersState state, DeleteCustomerSuccessAction action)
             => state with
             {
                 Customers = state.Customers.Where(x => x.Id != action.Id).ToImmutableArray(),

             };

        [ReducerMethod]
        public static CustomersState OnLoading(CustomersState state, CustomerLoadingAction action)
             => state with
             {
                 IsLoading = action.IsLoading
             };

        [ReducerMethod]
        public static CustomersState OnError(CustomersState state, CustomerErrorAction action)
             => state with
             {
                 Errors = action.Errors.ToImmutableArray()
             };


    }


}
