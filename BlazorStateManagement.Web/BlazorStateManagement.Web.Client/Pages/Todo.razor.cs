using BlazorStateManagement.Store;

using Fluxor;

using Microsoft.AspNetCore.Components;

namespace BlazorStateManagement.Web.Client.Pages
{
    public partial class Todo
    {

        [Inject]
        private IDispatcher Dispatcher { get; set; } = null!;

        [Inject]
        private IState<TodoState> State { get; set; } = null!;

        private string newTaskDescription;

        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(newTaskDescription))
            {
                Dispatcher.Dispatch(new AddTodoAction(newTaskDescription));
                Dispatcher.Dispatch(new AddNotificationAction("Se creo nueva Task: " + newTaskDescription));
                newTaskDescription = string.Empty;
            }
        }

        private void MarkComplete(Guid id)
        {
            Dispatcher.Dispatch(new CompleteTodoAction(id));
        }

        private void DeleteTask(Guid id)
        {
            Dispatcher.Dispatch(new DeleteTodoAction(id));
        }

    }
}
