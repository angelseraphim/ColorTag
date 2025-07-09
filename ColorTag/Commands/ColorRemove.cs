using CommandSystem;
using LabApi.Features.Wrappers;
using RemoteAdmin;
using System;
using System.Collections.Generic;
using static ColorTag.Data;

namespace ColorTag.Commands
{
    internal class ColorRemove : ICommand
    {
        public string Command { get; } = "remove";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "Remove colors";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = sender is PlayerCommandSender playerCommandSender
                ? Player.Get(playerCommandSender)
                : Server.Host;

            if (!Extensions.TryGetValue(player.UserId, out PlayerInfo info))
            {
                response = Plugin.config.Translation.NotFoundInDataBase;
                return false;
            }

            string text = string.Empty;

            List<string> colors = new List<string>();
            List<string> alreadyUsedColorsinforemove = info.Colors;

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
                alreadyUsedColorsinforemove.Remove(s);

            foreach (var s in alreadyUsedColorsinforemove)
                text += $"{s} ";

            info.Colors = alreadyUsedColorsinforemove;

            Extensions.PlayerInfoCollection.Update(info);

            player.GiveCoroutine();

            response = Plugin.config.Translation.Successfull
                .Replace("%current%", text);
            return true;
        }
    }
}
