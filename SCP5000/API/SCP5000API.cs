using Exiled.API.Features;
using System.Collections.Generic;

namespace SCP5000.API
{
    internal class API
    {
        public static List<Player> Players { get; private set; } = new List<Player>();

        public static void SpawnSCP5000(Player player) => player.GameObject.AddComponent<Component.PlayerComponent>();
    }
}