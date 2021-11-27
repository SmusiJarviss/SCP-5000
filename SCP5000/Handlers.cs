using Exiled.API.Features;
using MEC;
using System.Collections.Generic;
using System.Linq;

namespace SCP5000
{
    public class Handlers
    {
        public void RoundStarted()
        {
            Timing.CallDelayed(0.5f, () =>
            {
                if (API.SCP5000API.Players.Any() ||
                UnityEngine.Random.Range(0, 101) >= SCP5000.Singleton.Config.SpawnChance)
                    return;

                List<Player> players = Player.List.Where(x => x.Role == SCP5000.Singleton.Config.Role && !API.SCP5000API.Players.Contains(x)).ToList();

                if (players.IsEmpty()) return;
                Player player = players[UnityEngine.Random.Range(0, players.Count)];

                API.SCP5000API.SpawnSCP5000(player);
                API.SCP5000API.Players.Add(player);
            });

        }
    }
}