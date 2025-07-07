using LabApi.Features.Wrappers;
using LiteDB;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ColorTag.Data;

namespace ColorTag
{
    internal static class Extensions
    {
        internal static ILiteCollection<PlayerInfo> PlayerInfoCollection => Plugin.database.GetCollection<PlayerInfo>($"ColorSetting{Server.Port}");

        internal static async Task InsertPlayerAsync(Player player, List<string> colors)
        {
            PlayerInfo insert = new PlayerInfo()
            {
                UserId = player.UserId,
                Colors = new List<string>() { player.GroupColor }
            };

            await Task.Run(() => PlayerInfoCollection.Insert(insert));
        }

        internal static bool TryGetValue(string userId, out PlayerInfo info)
        {
            info = PlayerInfoCollection.FindById(userId);
            return info != null;
        }

        internal static bool Contains(string userId) => PlayerInfoCollection.FindById(userId) != null;

        internal static void DeletePlayer(string userId)
        {
            if (Contains(userId))
                PlayerInfoCollection.Delete(userId);
        }

        internal static void DeleteAll() => PlayerInfoCollection.DeleteAll();
    }
}
