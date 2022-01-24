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
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/></returns>
        public static bool TrySpawnSCP5000(this Player player)
        {
            if (Players.Contains(player) || player is null)
                return false;

            player.GameObject.AddComponent<Component.SCP5000Component>();
            return true;
        }

        /// <summary>
        /// Try kill <see cref="Player"/> as SCP-5000.
        /// </summary>
        /// <param name="player"> to kill.</param>
        /// <returns><see langword="true"/> if successful, otherwise <see langword="false"/></returns>
        public static bool TryKillScp5000(this Player player)
        {
            if (!Players.Contains(player) || player is null)
                return false;

            player.Kill("Killed by Server.");
            if (player.GameObject.TryGetComponent(out Component.SCP5000Component PlayerComponent)) PlayerComponent.Destroy();
            return true;
        }
    }
}