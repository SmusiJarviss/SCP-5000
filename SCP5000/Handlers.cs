using Exiled.API.Features;
using SCP5000.Component;
using System.Collections.Generic;
using System.Linq;

namespace SCP5000
{
    public class Handlers
    {
        public void RoundStarted()
        {
            if (API.API.Players.Any() ||
            UnityEngine.Random.Range(0, 101) > SCP5000.Singleton.Config.SpawnChance)
                return;

            List<Player> players = Player.List.Where(x => x.Role == RoleType.FacilityGuard && !API.API.Players.Contains(x)).ToList();

            if (players.IsEmpty()) return;
            Player player = players[UnityEngine.Random.Range(0, players.Count)];

            player.GameObject.AddComponent<PlayerComponent>();
        }
    }
}