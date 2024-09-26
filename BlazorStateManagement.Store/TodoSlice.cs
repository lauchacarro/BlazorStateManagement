using System.Collections.Immutable;

using BlazorStateManagement.Store.Extensions;

using Fluxor;

namespace BlazorStateManagement.Store
{


    [FeatureState]
    public record TodoState(ImmutableArray<TodoItem> Todos)
    {
        public static readonly TodoState Empty = new();

        private TodoState() :
            this(
                Todos: ImmutableArray.Create<TodoItem>())
        {
        }
    }


    public record TodoItem(Guid Id, string? Description, bool IsCompleted);



    public record AddTodoAction(string Description);

    public record CompleteTodoAction(Guid Id);

    public record DeleteTodoAction(Guid Id);


    

    public static class TodoReducers
    {

        [ReducerMethod]
        public static TodoState ReduceAddTodoAction(TodoState state, AddTodoAction action)
            => state with
            {
                Todos = state.Todos.Add(new TodoItem(Guid.NewGuid(), action.Description, false))
            };



        [ReducerMethod]
        public static TodoState ReduceCompleteTodoAction(TodoState state, CompleteTodoAction action)
            => !state.Todos.ReplaceOne(
                    selector: x => x.Id == action.Id,
                    replacement: x => x with { IsCompleted = true },
                    result: out var newTodos)
                ? state
                : state with { Todos = newTodos };




        [ReducerMethod]
        public static TodoState ReduceDeleteTodoAction(TodoState state, DeleteTodoAction action)
             => state with
             {
                 Todos = state.Todos.Where(x => x.Id != action.Id).ToImmutableArray()
             };

    }


}
