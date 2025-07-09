using LabApi.Events.Arguments.PlayerEvents;

namespace ColorTag.EventHandlers
{
    internal class PlayerEvents
    {
        internal void Register()
        {
            LabApi.Events.Handlers.PlayerEvents.Joined += OnJoined;
            LabApi.Events.Handlers.PlayerEvents.GroupChanged += OnGroupChanged;

        }

        internal void Unregister()
        {
            LabApi.Events.Handlers.PlayerEvents.Joined -= OnJoined;
            LabApi.Events.Handlers.PlayerEvents.GroupChanged -= OnGroupChanged;
        }

        private void OnJoined(PlayerJoinedEventArgs ev) => ev.Player.GiveCoroutine();

        private void OnGroupChanged(PlayerGroupChangedEventArgs ev) => ev.Player.GiveCoroutine();
    }
}
