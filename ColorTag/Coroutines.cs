using LabApi.Features.Wrappers;
using MEC;
using System.Collections.Generic;
using System.Linq;

namespace ColorTag
{
    internal static class Coroutines
    {
        internal static IEnumerator<float> ChangeColor(Player player, IEnumerable<string> colors)
        {
            int currentIndex = 0;

            while (player.IsOnline)
            {
                yield return Timing.WaitForSeconds(Plugin.config.Interval);

                if (currentIndex >= colors.Count())
                    currentIndex = 0;

                player.GroupColor = colors.ElementAt(currentIndex);
                currentIndex++;
            }

            yield break;
        }
    }
}
