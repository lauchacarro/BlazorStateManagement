using System.Collections.Immutable;

using Fluxor;

using Microsoft.AspNetCore.Components;

namespace BlazorStateManagement.Store
{
    public record NotificationItem(Guid Id, string Description);

    [FeatureState]
    public record NotificationsState(ImmutableArray<NotificationItem> Notifications)
    {
        public static readonly NotificationsState Empty = new();

        private NotificationsState() :
            this(
                Notifications: ImmutableArray.Create<NotificationItem>())
        {
        }
    }


    public record AddNotificationAction(string Description);


    public static class NotificationActions
    {
        public static AddNotificationAction AddNotificationAction(this ComponentBase _, string description)
        {
            return new(description);
        }
    }

    public static class NotidicationReducers
    {

        [ReducerMethod]
        public static NotificationsState ReduceAddNotificationAction(NotificationsState state, AddNotificationAction action)
            => state with
            {
                Notifications = state.Notifications.Add(new NotificationItem(Guid.NewGuid(), action.Description))
            };


    }

}
