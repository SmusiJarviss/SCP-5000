using Exiled.API.Features;
using System.Collections.Generic;

namespace SCP5000.API
{
    public class SCP5000API
    {
        internal static List<Player> Players { get; } = new List<Player>();

        public static void SpawnSCP5000(Player player)
        {
            if (Players.Contains(player)) return;
            player.GameObject.AddComponent<Component.PlayerComponent>();
        }

        public static void KillSCP5000(Player player)
        {
            if (player.GameObject.TryGetComponent(out Component.PlayerComponent PlayerComponent)) PlayerComponent.Destroy();
        }
    }
}