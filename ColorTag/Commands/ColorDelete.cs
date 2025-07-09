using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using RemoteAdmin;
using System;
using static ColorTag.Data;

namespace ColorTag.Commands
{
    internal class ColorDelete : ICommand
    {
        public string Command { get; } = "delete";
        public string[] Aliases { get; } = { "del" };
        public string Description { get; } = "Delete player data";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = sender is PlayerCommandSender playerCommandSender
                ? Player.Get(playerCommandSender)
                : Server.Host;

            if (!player.HasPermissions(Plugin.config.AdminRequirePermission))
            {
                response = Plugin.config.Translation.DontHavePermissions
                    .Replace("%permission%", Plugin.config.AdminRequirePermission);
                return false;
            }

            if (arguments.Count != 1)
            {
                response = "Using: colortag delete (UserID/all)";
                return false;
            }

            switch (arguments.At(0))
            {
                case "all":
                    if (!player.HasPermissions(Plugin.config.DropDataRequirePermission))
                    {
                        response = Plugin.config.Translation.DontHavePermissions
                            .Replace("%permission%", Plugin.config.DropDataRequirePermission);
                        return false;
                    }

                    Extensions.DeleteAll();

                    response = Plugin.config.Translation.KillDataBase;
                    return true;

                default:
                    if (!Extensions.TryGetValue(arguments.At(0), out PlayerInfo info))
                    {
                        response = Plugin.config.Translation.OtherNotFound;
                        return false;
                    }

                    Extensions.DeletePlayer(arguments.At(0));

                    response = Plugin.config.Translation.SuccessfullDeleted
                        .Replace("%userid%", arguments.At(0));
                    return true;
            }
        }
    }
}
