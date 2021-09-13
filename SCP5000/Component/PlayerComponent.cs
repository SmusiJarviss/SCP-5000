using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs;
using MEC;
using System;
using UnityEngine;

namespace SCP5000.Component
{
    internal class PlayerComponent : MonoBehaviour
    {
        internal Player Player { get; private set; }

        private void Awake()
        {
            SubscribeEvents();
            Player = Player.Get(gameObject);
            API.SCP5000API.Players.Add(Player);
        }

        private void Start() => SpawnSCP5000();

        private void Update()
        {
            if (Player.Role != RoleType.FacilityGuard)
                Destroy();
        }

        private void PartiallyDestroy()
        {
            UnsubscribeEvents();
            API.SCP5000API.Players.Remove(Player);
            Player.IsBypassModeEnabled = false;
            Player.RankName = null;
            Player.RankColor = null;
        }

        private void OnDestroy() => PartiallyDestroy();

        public void Destroy()
        {
            try
            {
                Destroy(this);
            }
            catch (Exception e)
            {
                Log.Error($"Couldn't destroy JumpComponent: {e}");
            }
        }

        private void SubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Died += OnDied;
            Exiled.Events.Handlers.Player.TriggeringTesla += OnTriggeringTesla;
            Exiled.Events.Handlers.Player.EnteringFemurBreaker += OnEnteringFemurBreaker;
            Exiled.Events.Handlers.Player.Dying += OnDying;
            Exiled.Events.Handlers.Scp096.AddingTarget += OnAddingTarget;
        }

        private void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Died -= OnDied;
            Exiled.Events.Handlers.Player.TriggeringTesla -= OnTriggeringTesla;
            Exiled.Events.Handlers.Player.EnteringFemurBreaker -= OnEnteringFemurBreaker;
            Exiled.Events.Handlers.Player.Dying -= OnDying;
            Exiled.Events.Handlers.Scp096.AddingTarget -= OnAddingTarget;
        }

        internal void OnDied(DiedEventArgs ev)
        {
            if (ev.Target != Player) return;
            Cassie.Message(SCP5000.Singleton.Config.RecontainCassie, false, true);
            Destroy();
        }

        internal void SpawnSCP5000()
        {
            if (Player.Role != RoleType.FacilityGuard)
                Player.Role = RoleType.FacilityGuard;
            Player.Broadcast(SCP5000.Singleton.Config.SpawnBroadcast.Duration, SCP5000.Singleton.Config.SpawnBroadcast.Content.Replace("{player}", Player.Nickname), Broadcast.BroadcastFlags.Normal, true);
            Timing.CallDelayed(0.5f, () => Player.ResetInventory(SCP5000.Singleton.Config.Inventory));
            Player.Ammo.Add(ItemType.Ammo762x39, 40);
            if (SCP5000.Singleton.Config.EnableEffect)
            {
                Player.EnableEffect(EffectType.SinkHole);
                Player.EnableEffect(EffectType.Deafened);
            }
            Player.IsBypassModeEnabled = true;
            Player.Health = Player.MaxHealth = SCP5000.Singleton.Config.HP;
            Cassie.Message(SCP5000.Singleton.Config.SpawnCassie, false, true);
            SetBadge();
        }

        internal void SetBadge()
        {
            Player.BadgeHidden = false;
            Player.RankName = SCP5000.Singleton.Config.Badge;
            Player.RankColor = SCP5000.Singleton.Config.Color;
        }

        public void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if (ev.Player != Player) return;
            if (API.SCP5000API.Players.Contains(Player) && !SCP5000.Singleton.Config.TeslaTriggerable)
            {
                ev.IsTriggerable = false;
                Player.Broadcast(SCP5000.Singleton.Config.TeslaBroadcast.Duration, SCP5000.Singleton.Config.TeslaBroadcast.Content.Replace("{player}", ev.Player.Nickname), Broadcast.BroadcastFlags.Normal, true);
            }
        }

        public void OnEnteringFemurBreaker(EnteringFemurBreakerEventArgs ev)
        {
            if (ev.Player != Player) return;
            if (API.SCP5000API.Players.Contains(Player) && !SCP5000.Singleton.Config.FemurBreakerTriggerable)
            {
                ev.IsAllowed = false;
                Player.Broadcast(SCP5000.Singleton.Config.FemurBreakerBroadcast.Duration, SCP5000.Singleton.Config.FemurBreakerBroadcast.Content.Replace("{player}", ev.Player.Nickname), Broadcast.BroadcastFlags.Normal, true);
            }
        }

        public void OnDying(DyingEventArgs ev)
        {
            if (ev.Target != Player) return;

            if (API.SCP5000API.Players.Contains(Player) && SCP5000.Singleton.Config.ExplosionEnable)
            {
                Cassie.Message(SCP5000.Singleton.Config.ExplosionCassie);
                new ExplosiveGrenade(ItemType.GrenadeHE, Player) { FuseTime = SCP5000.Singleton.Config.FuseTime }.SpawnActive(Player.Position, Player);
            }
        }

        public void OnAddingTarget(AddingTargetEventArgs ev)
        {
            if (ev.Target != Player) return;
            if (API.SCP5000API.Players.Contains(Player) && !SCP5000.Singleton.Config.AddingTarget)
            {
                ev.IsAllowed = false;
                Player.Broadcast(SCP5000.Singleton.Config.AddingTargetBroadcast.Duration, SCP5000.Singleton.Config.AddingTargetBroadcast.Content, Broadcast.BroadcastFlags.Normal, true);
            }
        }
    }
}