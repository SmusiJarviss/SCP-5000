using Exiled.API.Enums;
using Exiled.API.Features;
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
            API.API.Players.Add(Player);
        }

        private void Start() => SpawnSCP5000();

        private void Update()
        {
            if (Player.Role != RoleType.FacilityGuard)
                Destroy();
        }

        private void FixedUpdate()
        {
            if (Player != null) return;
            Destroy();
            return;
        }

        private void PartiallyDestroy()
        {
            UnsubscribeEvents();
            API.API.Players.Remove(Player);
            RemoveBadge();
            if (Player is null) return;
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
        }

        private void UnsubscribeEvents()
        {
            Exiled.Events.Handlers.Player.Died -= OnDied;
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
            Player.Ammo.Add(ItemType.Ammo556x45, 250);
            if (SCP5000.Singleton.Config.EnableEffect)
            {
                Player.EnableEffect(EffectType.SinkHole);
                Player.EnableEffect(EffectType.Deafened);
            }
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

        internal void RemoveBadge()
        {
            Player.RankName = null;
            Player.RankColor = null;
        }
    }
}