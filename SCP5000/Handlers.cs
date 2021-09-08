using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs;
using SCP5000.Component;
using System.Collections.Generic;
using System.Linq;

namespace SCP5000
{
    public class Handlers
    {

        public void RoundStarted()
        {
            List<Player> players = Player.List.Where(x => x.Role == RoleType.FacilityGuard && !API.API.Players.Contains(x)).ToList();

            if (API.API.Players.Any() ||
            UnityEngine.Random.Range(0, 101) > SCP5000.Singleton.Config.SpawnChance)
                return;

            if (players.IsEmpty()) return;
            Player player = players[UnityEngine.Random.Range(0, players.Count)];

            player.GameObject.AddComponent<PlayerComponent>();
        }

        public void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if (!API.API.Players.Contains(ev.Player)) return;

            ev.IsTriggerable = SCP5000.Singleton.Config.TeslaTriggerable;
            ev.Player.Broadcast(SCP5000.Singleton.Config.TeslaBroadcast.Duration, SCP5000.Singleton.Config.TeslaBroadcast.Content.Replace("{player}", ev.Player.Nickname), Broadcast.BroadcastFlags.Normal, true);
        }

        public void OnEnteringFemurBreaker(EnteringFemurBreakerEventArgs ev)
        {
            if (!API.API.Players.Contains(ev.Player)) return;

            ev.IsAllowed = SCP5000.Singleton.Config.FemurBreakerTriggerable;
            ev.Player.Broadcast(SCP5000.Singleton.Config.FemurBreakerBroadcast.Duration, SCP5000.Singleton.Config.FemurBreakerBroadcast.Content.Replace("{player}", ev.Player.Nickname), Broadcast.BroadcastFlags.Normal, true);
        }

        public void OnDying(DyingEventArgs ev)
        {
            if (!API.API.Players.Contains(ev.Target)) return;

            Cassie.Message(SCP5000.Singleton.Config.ExplosionCassie);
            for (int i = 0; i < SCP5000.Singleton.Config.ExplosionNumber; i++)
            {
                new ExplosiveGrenade(ItemType.GrenadeHE, ev.Target) { FuseTime = SCP5000.Singleton.Config.FuseTime }.SpawnActive(ev.Target.Position, ev.Target);
            }
        }

        public void OnAddingTarget(AddingTargetEventArgs ev)
        {
            if (!API.API.Players.Contains(ev.Target)) return;

            ev.IsAllowed = SCP5000.Singleton.Config.AddingTarget;
            ev.Target.Broadcast(SCP5000.Singleton.Config.AddingTargetBroadcast.Duration, SCP5000.Singleton.Config.AddingTargetBroadcast.Content, Broadcast.BroadcastFlags.Normal, true);
        }
    }
}