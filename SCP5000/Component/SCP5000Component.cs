using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs;
using MEC;
using System;
using System.Linq;
using UnityEngine;
using PlayerEvents = Exiled.Events.Handlers.Player;

namespace SCP5000.Component
{
    internal class SCP5000Component : MonoBehaviour
    {
        internal Player Player { get; private set; }

        private void Awake()
        {
            SubscribeEvents();
            Player = Player.Get(gameObject);
            Player.SessionVariables.Add("scp5000", true);
        }

        private void Start()
        {
            if (Player.Role != SCP5000.Singleton.Config.Role)
                Player.Role = SCP5000.Singleton.Config.Role;

            Player.Broadcast(SCP5000.Singleton.Config.SpawnBroadcast.Duration, SCP5000.Singleton.Config.SpawnBroadcast.Content.Replace("{player}", Player.Nickname), Broadcast.BroadcastFlags.Normal, true);

            Timing.CallDelayed(0.5f, () => Player.ResetInventory(SCP5000.Singleton.Config.Inventory));
            Timing.CallDelayed(0.5f, () => Player.Position = Map.Rooms.FirstOrDefault(x => x.Type == SCP5000.Singleton.Config.SpawnRoom).Position + Vector3.up * 1.5f);

            Player.EnableEffects(SCP5000.Singleton.Config.Effects);
            Player.Ammo.Add(ItemType.Ammo762x39, 40);

            Player.IsBypassModeEnabled = SCP5000.Singleton.Config.EnableBypass;
            Player.Health = Player.MaxHealth = SCP5000.Singleton.Config.HP;

            if (SCP5000.Singleton.Config.BadgeEnabled)
            {
                Player.BadgeHidden = false;
                Player.RankName = SCP5000.Singleton.Config.Badge;
                Player.RankColor = SCP5000.Singleton.Config.Color;
            }

            if (SCP5000.Singleton.Config.EnableCassie)
                Cassie.Message(SCP5000.Singleton.Config.SpawnCassie, false, true);
        }

        private void FixedUpdate()
        {
            if (Player is null || Player.Role != SCP5000.Singleton.Config.Role)
                Destroy();
        }

        private void PartiallyDestroy()
        {
            UnsubscribeEvents();
            if (Player is null) return;

            Player.SessionVariables.Remove("scp5000");
            Player.IsBypassModeEnabled = false;
            Player.RankName = default;
            Player.RankColor = default;
            Player.DisableAllEffects();
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
                Log.Error($"Couldn't destroy SCP5000Component: {e}");
            }
        }

        private void SubscribeEvents()
        {
            PlayerEvents.Died += OnDied;
            PlayerEvents.TriggeringTesla += OnTriggeringTesla;
            PlayerEvents.EnteringFemurBreaker += OnEnteringFemurBreaker;
            PlayerEvents.Dying += OnDying;
            Exiled.Events.Handlers.Scp096.AddingTarget += OnAddingTarget;
        }

        private void UnsubscribeEvents()
        {
            PlayerEvents.Died -= OnDied;
            PlayerEvents.TriggeringTesla -= OnTriggeringTesla;
            PlayerEvents.EnteringFemurBreaker -= OnEnteringFemurBreaker;
            PlayerEvents.Dying -= OnDying;
            Exiled.Events.Handlers.Scp096.AddingTarget -= OnAddingTarget;
        }

        private void OnDied(DiedEventArgs ev)
        {
            if (ev.Target != Player) return;
            if (SCP5000.Singleton.Config.EnableCassie)
            {
                Cassie.Message(SCP5000.Singleton.Config.RecontainCassie, false, true);
            }
        }

        private void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if (ev.Player != Player || SCP5000.Singleton.Config.TeslaTriggerable) return;

            ev.IsTriggerable = false;
            Player.Broadcast(SCP5000.Singleton.Config.TeslaBroadcast.Duration, SCP5000.Singleton.Config.TeslaBroadcast.Content.Replace("{player}", ev.Player.Nickname), Broadcast.BroadcastFlags.Normal, true);
        }

        private void OnEnteringFemurBreaker(EnteringFemurBreakerEventArgs ev)
        {
            if (ev.Player != Player || SCP5000.Singleton.Config.FemurBreakerTriggerable) return;

            ev.IsAllowed = false;
            Player.Broadcast(SCP5000.Singleton.Config.FemurBreakerBroadcast.Duration, SCP5000.Singleton.Config.FemurBreakerBroadcast.Content.Replace("{player}", ev.Player.Nickname), Broadcast.BroadcastFlags.Normal, true);
        }

        private void OnDying(DyingEventArgs ev)
        {
            if (ev.Target != Player || !SCP5000.Singleton.Config.ExplosionEnable) return;
            Player.Health = 100;

            for (int i = 0; i < SCP5000.Singleton.Config.ExplosionNumber; i++)
            {
                new ExplosiveGrenade(ItemType.GrenadeHE, Player) { FuseTime = SCP5000.Singleton.Config.FuseTime }.SpawnActive(Player.Position, Player);
            }

            if (SCP5000.Singleton.Config.EnableCassie)
                Cassie.Message(SCP5000.Singleton.Config.ExplosionCassie);
        }

        private void OnAddingTarget(AddingTargetEventArgs ev)
        {
            if (ev.Target != Player || SCP5000.Singleton.Config.CanBe096Target) return;

            ev.IsAllowed = false;
            Player.Broadcast(SCP5000.Singleton.Config.CanBe096Broadcast.Duration, SCP5000.Singleton.Config.CanBe096Broadcast.Content, Broadcast.BroadcastFlags.Normal, true);
        }
    }
}