using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using RemoteAdmin;
using System;
using static ColorTag.Data;

namespace ColorTag.Commands
{
    public class ColorCheck : ICommand
    {
        public string Command { get; } = "check";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "Check player settings";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = sender is PlayerCommandSender playerCommandSender
                ? Player.Get(playerCommandSender)
                : Server.Host;

            if (!player.HasPermissions(Plugin.config.AdminRequirePermission))
            {
                response = Plugin.config.Translation.DontHavePermissions.Replace("%permission%", Plugin.config.AdminRequirePermission);
                return false;
            }

            if (arguments.Count != 1)
            {
                response = "Using: colortag check (UserID)";
                return false;
            }

            if (!Extensions.TryGetValue(arguments.At(0), out PlayerInfo info))
            {
                response = Plugin.config.Translation.OtherNotFound;
                return false;
            }

            string text = $"{info.UserId}";

            foreach(string s in info.Colors)
                text += $"\n{s}";

            response = text;
            return true;
        }
    }
}
