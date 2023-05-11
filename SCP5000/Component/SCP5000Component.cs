using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp096;
using MEC;
using System;
using UnityEngine;
using PlayerEvents = Exiled.Events.Handlers.Player;

namespace SCP5000.Component
{
    internal class SCP5000Component : MonoBehaviour
    {
        private int grenadeCounter;

        internal Player Player { get; private set; }
        public LineRenderer LineRenderer;

        private void Awake()
        {
            SubscribeEvents();
            Player = Player.Get(gameObject);
            Player.SessionVariables.Add("scp5000", true);
            LineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        private void Start()
        {
            if (Player.Role.Type != SCP5000.Singleton.Config.Role)
                Player.Role.Set(SCP5000.Singleton.Config.Role);

            Player.Broadcast(SCP5000.Singleton.Config.SpawnBroadcast.Duration, SCP5000.Singleton.Config.SpawnBroadcast.Content.Replace("{player}", Player.Nickname), Broadcast.BroadcastFlags.Normal, true);

            Timing.CallDelayed(0.5f, () =>
            {
                Player.ResetInventory(SCP5000.Singleton.Config.Inventory);
                Player.Position = Room.Get(SCP5000.Singleton.Config.SpawnRoom).Position;
            });

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
            if (Player is null)
                return;

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
            PlayerEvents.Dying += OnDying;
            PlayerEvents.TogglingNoClip += OnTogglingNoClip;
            PlayerEvents.UsingItem += OnUsingItem;
            PlayerEvents.DroppingItem += OnDroppingItem;
            PlayerEvents.ProcessingHotkey += OnProcessingHotKey;
            Exiled.Events.Handlers.Scp096.AddingTarget += OnAddingTarget;
        }

        private void UnsubscribeEvents()
        {
            PlayerEvents.Died -= OnDied;
            PlayerEvents.TriggeringTesla -= OnTriggeringTesla;
            PlayerEvents.Dying -= OnDying;
            PlayerEvents.TogglingNoClip -= OnTogglingNoClip;
            PlayerEvents.UsingItem -= OnUsingItem;
            PlayerEvents.DroppingItem -= OnDroppingItem;
            PlayerEvents.ProcessingHotkey -= OnProcessingHotKey;
            Exiled.Events.Handlers.Scp096.AddingTarget -= OnAddingTarget;
        }

        private void OnDied(DiedEventArgs ev)
        {
            if (SCP5000.Singleton.Config.EnableCassie && ev.Attacker != Player)
                Cassie.Message(SCP5000.Singleton.Config.RecontainCassie, false, true);
        }

        private void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if (ev.Player != Player || SCP5000.Singleton.Config.TeslaTriggerable)
                return;

            ev.IsTriggerable = false;
            Player.Broadcast(SCP5000.Singleton.Config.TeslaBroadcast.Duration, SCP5000.Singleton.Config.TeslaBroadcast.Content.Replace("{player}", ev.Player.Nickname), Broadcast.BroadcastFlags.Normal, true);
        }

        private void OnDying(DyingEventArgs ev)
        {
            if (ev.Attacker != Player || !SCP5000.Singleton.Config.ExplosionEnable)
                return;

            for (int i = 0; i < SCP5000.Singleton.Config.ExplosionNumber; i++)
            {
                ExplosiveGrenade grenade = (ExplosiveGrenade)Item.Create(ItemType.GrenadeHE, Player);
                grenade.FuseTime = SCP5000.Singleton.Config.FuseTime;
                grenade.SpawnActive(Player.Position + Vector3.up * 1.5f);
            }

            if (SCP5000.Singleton.Config.EnableCassie)
                Cassie.Message(SCP5000.Singleton.Config.ExplosionCassie);
        }

        private void OnAddingTarget(AddingTargetEventArgs ev)
        {
            if (ev.Target != Player || SCP5000.Singleton.Config.CanTrigger096)
                return;

            ev.IsAllowed = false;
            Player.Broadcast(SCP5000.Singleton.Config.CanTrigger096Broadcast.Duration, SCP5000.Singleton.Config.CanTrigger096Broadcast.Content, Broadcast.BroadcastFlags.Normal, true);
        }

        private void OnUsingItem(UsingItemEventArgs ev)
        {
            if (ev.Player != Player || ev.Item.Type != ItemType.GrenadeHE)
                return;

            ev.IsAllowed = false;
        }

        private void OnDroppingItem(DroppingItemEventArgs ev)
        {
            if (ev.Player != Player || ev.Item.Type != ItemType.GrenadeHE)
                return;

            ev.IsAllowed = false;
        }

        private void OnProcessingHotKey(ProcessingHotkeyEventArgs ev)
        {
            if (ev.Player != Player || ev.Hotkey != HotkeyButton.Grenade)
                return;

            Player.ShowHitMarker(10f);
            Player.EnableEffect((EffectType)18, 10);
            Player.Teleport(Room.Get(SCP5000.Singleton.Config.TeleportRooms[UnityEngine.Random.Range(0, SCP5000.Singleton.Config.TeleportRooms.Count)]).Position + (Vector3.up * 1.5f));
            Player.Health -= 250;
            ev.IsAllowed = false;
        }

        private void OnTogglingNoClip(TogglingNoClipEventArgs ev)
        {
            if (ev.Player != Player || grenadeCounter >= SCP5000.Singleton.Config.GrenadeLimit)
                return;

            Physics.Raycast(ev.Player.Position, ev.Player.CameraTransform.forward, out RaycastHit raycastHit);
            ((ExplosiveGrenade)Item.Create(ItemType.GrenadeHE)).SpawnActive(raycastHit.point + (Vector3.up * 1.5f));

            grenadeCounter++;
            Player.ShowHitMarker(5f);
            Player.Broadcast(SCP5000.Singleton.Config.GrenadeBroadcast.Duration, SCP5000.Singleton.Config.GrenadeBroadcast.Content.Replace("%num", (grenadeCounter + "/" + SCP5000.Singleton.Config.GrenadeLimit).ToString()));

            ev.IsAllowed = false;
        }
    }
}