using Exiled.API.Features;
using System.Collections.Generic;
using System.Linq;

namespace SCP5000.API
{
    public static class SCP5000API
    {
        /// <summary>
        /// <see cref="Player"/> as SCP-5000.
        /// </summary>
        public static HashSet<Player> Players => Player.List.Where(player => player.SessionVariables.ContainsKey("scp5000")).ToHashSet();

        /// <summary>
        /// Check if <see cref="Player"/> is SCP-5000.
        /// </summary>
        /// <param name="player"> to check.</param>
        /// <returns><see langword="true"/> if is SCP-5000, otherwise <see langword="false"/></returns>
        public static bool IsScp5000(this Player player) => player.SessionVariables.ContainsKey("scp5000");

        /// <summary>
        /// Try spawn <see cref="Player"/> as SCP-5000.
        /// </summary>
        /// <param name="player"> to spawn.</param>
        public static void TrySpawnSCP5000(this Player player)
        {
            if (Players.Contains(player) || player is null)
                return;

            player.SessionVariables.Add("scp5000", true);
            player.GameObject.AddComponent<Component.SCP5000Component>();
        }

        /// <summary>
        /// Try kill <see cref="Player"/> as SCP-5000.
        /// </summary>
        /// <param name="player"> to kill.</param>
        public static void TryKillScp5000(this Player player)
        {
            if (!Players.Contains(player) || player is null)
                return;

            player.Kill("Killed by Server.");
            player.SessionVariables.Remove("scp5000");
            if (player.GameObject.TryGetComponent(out Component.SCP5000Component PlayerComponent)) PlayerComponent.Destroy();
        }
    }
}