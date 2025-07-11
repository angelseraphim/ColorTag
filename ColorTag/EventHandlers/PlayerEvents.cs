using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Console;
using MEC;

namespace ColorTag.EventHandlers
{
    internal class PlayerEvents
    {
        internal void Register()
        {
            LabApi.Events.Handlers.PlayerEvents.Joined += OnJoined;
            LabApi.Events.Handlers.PlayerEvents.GroupChanged += OnGroupChanged;
            LabApi.Events.Handlers.PlayerEvents.Left += OnLeft;
        }

        internal void Unregister()
        {
            LabApi.Events.Handlers.PlayerEvents.Joined -= OnJoined;
            LabApi.Events.Handlers.PlayerEvents.GroupChanged -= OnGroupChanged;
            LabApi.Events.Handlers.PlayerEvents.Left += OnLeft;
        }

        private void OnLeft(PlayerLeftEventArgs ev)
        {
            if (!Plugin.PlayerCoroutines.TryGetValue(ev.Player, out CoroutineHandle coroutine))
                return;

            if (coroutine.IsRunning)
                Timing.KillCoroutines(coroutine);

            Plugin.PlayerCoroutines.Remove(ev.Player);
        }

        private void OnJoined(PlayerJoinedEventArgs ev) => ev.Player.GiveCoroutine();

        private void OnGroupChanged(PlayerGroupChangedEventArgs ev) => ev.Player.GiveCoroutine();
    }
}
