using LabApi.Features.Console;
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
            IList<string> colorList = colors as IList<string> ?? colors.ToList();

            if (colorList.Count <= 0)
                yield break;

            int currentIndex = 0;

            while (player.ReferenceHub != null)
            {
                yield return Timing.WaitForSeconds(Plugin.config.Interval);

                if (currentIndex >= colorList.Count)
                    currentIndex = 0;

                if (player != null && player.IsOnline)
                    player.GroupColor = colorList[currentIndex];

                currentIndex++;
            }

            yield break;
        }
    }
}
