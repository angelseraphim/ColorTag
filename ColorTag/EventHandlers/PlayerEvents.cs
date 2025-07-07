using LabApi.Events.Arguments.PlayerEvents;

namespace ColorTag.EventHandlers
{
    internal class PlayerEvents
    {
        internal void Register() => LabApi.Events.Handlers.PlayerEvents.Joined += OnJoined;

        internal void Unregister() => LabApi.Events.Handlers.PlayerEvents.Joined -= OnJoined;

        private void OnJoined(PlayerJoinedEventArgs ev) => Plugin.GiveCoroutine(ev.Player);
    }
}
