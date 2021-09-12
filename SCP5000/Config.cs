using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace SCP5000
{
    public class Config : IConfig
    {
        [Description("Plugin Enable/Disable.")]
        public bool IsEnabled { get; set; } = true;

        [Description("Spawnchance of SCP-5000.")]
        public float SpawnChance { get; set; } = 60f;

        [Description("Broadcast when you spawn as SCP-5000.")]
        public Exiled.API.Features.Broadcast SpawnBroadcast { get; set; } = new Exiled.API.Features.Broadcast("<size=30><b><color=cyan>{player}</color> you has spawned as <color=red>SCP-5000</color></b></size>", 5);

        [Description("Cassie when you spawn as SCP-5000.")]
        public string SpawnCassie { get; set; } = "pitch_0.949 .g3 scp 5 0 0 0 breached containment .g3";

        [Description("Editable inventory of SCP-5000.")]
        public List<ItemType> Inventory { get; set; } = new List<ItemType>()
        {
            ItemType.GunAK,
            ItemType.Medkit,
            ItemType.Flashlight,
            ItemType.GrenadeHE,
        };

        [Description("Enable/Diable the Player Effect.")]
        public bool EnableEffect { get; set; } = true;

        [Description("Set Badge Color.")]
        public string Color { get; set; } = "red";

        [Description("Set Badge Name.")]
        public string Badge { get; set; } = "SCP-5000";

        [Description("Set HP of SCP-5000.")]
        public int HP { get; set; } = 1500;

        [Description("Set if SCP-5000 can trigger Tesla Gate.")]
        public bool TeslaTriggerable { get; set; } = false;

        [Description("Broadcast when SCP-5000 go near a Tesla Gate. Only avaible when <TeslaTriggerable> has been set to <false>")]
        public Exiled.API.Features.Broadcast TeslaBroadcast { get; set; } = new Exiled.API.Features.Broadcast("<size=30><b><color=cyan>{player}</color> deactivating <color=red>Tesla Gate...</color></b></size>", 5);

        [Description("Set if SCP-5000 can enter the Femur Breaker.")]
        public bool FemurBreakerTriggerable { get; set; } = false;

        [Description("Set the Broadcast when SCP-5000 attempt to enter the Femur Breaker. Only avaible when <FemurBreakerTriggerable> has been set to <false>")]
        public Exiled.API.Features.Broadcast FemurBreakerBroadcast { get; set; } = new Exiled.API.Features.Broadcast("<size=30><b><color=cyan>{player}</color> SCP-5000 protect you from the <color=red>Femur Breaker</color></b></size>", 5);

        [Description("Cassie when SCP-5000 automatic destruction start.")]
        public string ExplosionCassie { get; set; } = "pitch_0.949 .g3 scp 5 0 0 0 automatic destruction in 5 . 4 . 3 . 2 . 1";

        [Description("Set the fuse delay of auto destruction. (Delay of the Explosion)")]
        public float FuseTime { get; set; } = 14.5f;

        [Description("ExplosionEnable set if auto destruction is enabled.")]
        public bool ExplosionEnable { get; set; } = true;

        [Description("Cassie when SCP-5000 has been recontained.")]
        public string RecontainCassie { get; set; } = "pitch_0.949 .g3 scp 5 0 0 0 has been recontained successfully .g3";

        [Description("Set if SCP-5000 can be a target of SCP-096.")]
        public bool AddingTarget { get; set; } = false;

        [Description("Set the Broadcast when SCP-5000 protect you from become a target of SCP-096. Only avaible when <AddingTarget> has been set to <false>.")]
        public Exiled.API.Features.Broadcast AddingTargetBroadcast { get; set; } = new Exiled.API.Features.Broadcast("<size=30><b><color=cyan>SCP-5000</color> protect you from <color=red>SCP-096...</color></b></size>", 5);
    }
}