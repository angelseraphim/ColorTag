using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using static ColorTag.Data;

namespace ColorTag.Commands
{
    internal class ColorAdd : ICommand
    {
        public string Command { get; } = "add";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "Add color";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = sender is PlayerCommandSender playerCommandSender 
                ? Player.Get(playerCommandSender) 
                : Server.Host;

            if (!player.HasPermissions(Plugin.config.ColorRequirePermission))
            {
                response = Plugin.config.Translation.DontHavePermissions
                    .Replace("%permission%", Plugin.config.ColorRequirePermission);
                return false;
            }

            if (!Extensions.TryGetValue(player.UserId, out PlayerInfo info))
            {
                response = Plugin.config.Translation.NotFoundInDataBase;
                return false;
            }

            if (!Plugin.config.GroupColorLimit.TryGetValue(player.UserGroup.Name, out int limit))
                limit = Plugin.config.DefaultColorLimit;

            if (arguments.Count > limit)
            {
                response = Plugin.config.Translation.ColorLimit
                    .Replace("%limit%", limit.ToString());
                return false;
            }

            string text = string.Empty;

            List<string> colors = new List<string>();
            List<string> alreadyUsedColors = info.Colors;

            foreach (string arg in arguments)
            {
                if (!Plugin.AvailableColors.ContainsKey(arg))
                {
                    response = Plugin.config.Translation.InvalidColor
                        .Replace("%arg%", arg)
                        .Replace("%colors%", Plugin.ShowColors());
                    return false;
                }

                colors.Add(arg);
            }

            foreach (var s in colors)
                alreadyUsedColors.Add(s);

            foreach (var s in alreadyUsedColors)
                text += $"{s} ";

            info.Colors = alreadyUsedColors;

            Extensions.PlayerInfoCollection.Update(info);

            player.GiveCoroutine();

            response = Plugin.config.Translation.Successfull
                .Replace("%current%", text);
            return true;
        }
    }
}
