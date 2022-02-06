using Exiled.API.Enums;
using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace SCP5000
{
    public class Config : IConfig
    {
        [Description("Enable or disable SCP-5000.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Enable or disable all Cassie related to SCP-5000.")]
        public bool EnableCassie { get; set; } = true;

        [Description("Enable or disable the bypass mode that will be given to the player as SCP-5000.")]
        public bool EnableBypass { get; set; } = true;

        [Description("Chance to spawn as SCP-5000.")]
        public float SpawnChance { get; set; } = 60f;

        [Description("Broadcast that will be shown to the player when is spawned as SCP-5000.")]
        public Exiled.API.Features.Broadcast SpawnBroadcast { get; set; } = new Exiled.API.Features.Broadcast("<size=30><b><color=cyan>{player}</color> you has spawned as <color=red>SCP-5000</color></b></size>", 5);

        [Description("Cassie that will be reproduced when someone spawn as SCP-5000.")]
        public string SpawnCassie { get; set; } = "pitch_0.949 .g3 scp 5 0 0 0 breached containment .g3";

        [Description("Room where the player as SCP-5000 will be spawned.")]
        public RoomType SpawnRoom { get; set; } = RoomType.HczServers;

        [Description("What will be the player role as SCP-5000.")]
        public RoleType Role { get; set; } = RoleType.ClassD;

        [Description("Items that will be given to the player as SCP-5000.")]
        public List<ItemType> Inventory { get; set; } = new List<ItemType>()
        {
            ItemType.GunAK,
            ItemType.Medkit,
            ItemType.Flashlight,
            ItemType.GrenadeHE,
        };

        [Description("Effects that will be given to the player as SCP-5000.")]
        public List<EffectType> Effects { get; set; } = new List<EffectType>()
        {
            EffectType.Deafened,
            EffectType.Concussed,
        };

        [Description("Enable or disable the badge that will be given to the player as SCP-5000.")]
        public bool BadgeEnabled { get; set; } = true;

        [Description("Badge Color that will be given to the player as SCP-5000.")]
        public string Color { get; set; } = "red";

        [Description("Badge Name that will be given to the player as SCP-5000.")]
        public string Badge { get; set; } = "SCP-5000";

        [Description("Health that will be given to the player as SCP-5000.")]
        public int HP { get; set; } = 1500;

        [Description("Choose if SCP-5000 can trigger Tesla Gate.")]
        public bool TeslaTriggerable { get; set; } = false;

        [Description("Broadcast that will be shown to the player as SCP-5000 when go near a Tesla Gate. Only available when <TeslaTriggerable> has been set to false.")]
        public Exiled.API.Features.Broadcast TeslaBroadcast { get; set; } = new Exiled.API.Features.Broadcast("<size=30><b><color=cyan>{player}</color> deactivating <color=red>Tesla Gate...</color></b></size>", 5);

        [Description("Choose if SCP-5000 can enter the Femur Breaker.")]
        public bool FemurBreakerTriggerable { get; set; } = false;

        [Description("Broadcast that will be shown to the player as SCP-5000 when entering in the Femur Breaker. Only available when <FemurBreakerTriggerable> has been set to false.")]
        public Exiled.API.Features.Broadcast FemurBreakerBroadcast { get; set; } = new Exiled.API.Features.Broadcast("<size=30><b><color=cyan>{player}</color> SCP-5000 protect you from the <color=red>Femur Breaker</color></b></size>", 5);

        [Description("Cassie that will be reproduced when someone as SCP-5000 died.")]
        public string ExplosionCassie { get; set; } = "pitch_0.949 .g3 scp 5 0 0 0 automatic destruction in 5 . 4 . 3 . 2 . 1";

        [Description("Choose how many granades will be spawned to the player as SCP-5000 after dying. (low number raccomended)")]
        public int ExplosionNumber { get; set; } = 3;

        [Description("Choose the fuse delay of each granades. (Delay of the Explosion)")]
        public float FuseTime { get; set; } = 14.5f;

        [Description("Choose set if explosion after dying is enabled.")]
        public bool ExplosionEnable { get; set; } = true;

        [Description("Cassie that will be reproduced when SCP-5000 has been recontained.")]
        public string RecontainCassie { get; set; } = "pitch_0.949 .g3 scp 5 0 0 0 has been recontained successfully .g3";

        [Description("Choose if player as SCP-5000 can be a target of SCP-096.")]
        public bool CanTrigger096 { get; set; } = false;

        [Description("Broadcast that will be shown to the player as SCP-5000 after seeing SCP-096. Only available when <AddingTarget> has been set to false.")]
        public Exiled.API.Features.Broadcast CanTrigger096Broadcast { get; set; } = new Exiled.API.Features.Broadcast("<size=30><b><color=cyan>SCP-5000</color> protect you from <color=red>SCP-096...</color></b></size>", 5);
    }
}