using CommandSystem;
using LabApi.Features.Permissions;
using LabApi.Features.Wrappers;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using static ColorTag.Data;

namespace ColorTag.Commands
{
    internal class ColorSet : ICommand
    {
        public string Command { get; } = "set";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "Set colors";

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

            if (arguments.Count < 1)
            {
                response = "Usage: colortag set (colors)";
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

            List<string> colors = new List<string>();

            foreach (string arg in arguments)
            {
                if (string.IsNullOrEmpty(arg))
                {
                    response = "Один из переданных цветов пустой или некорректный.";
                    return false;
                }

                if (!Plugin.AvailableColors.ContainsKey(arg))
                {
                    response = Plugin.config.Translation.InvalidColor
                        .Replace("%arg%", arg)
                        .Replace("%colors%", Plugin.ShowColors());
                    return false;
                }

                colors.Add(arg);
            }

            string text = string.Empty;

            foreach (var s in colors)
                text += $"{s} ";

            if (!Extensions.TryGetValue(player.UserId, out PlayerInfo info))
            {
                Extensions.InsertPlayerAsync(player, colors);
            }
            else
            {
                info.Colors = colors;
                Extensions.PlayerInfoCollection.Update(info);
            }

            player.GiveCoroutine();

            response = Plugin.config.Translation.Successfull
                .Replace("%current%", text);
            return true;
        }
    }
}
