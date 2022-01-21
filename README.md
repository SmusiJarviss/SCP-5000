
# SCP-5000

Plugin for SCP: Secret Laboratory, work with Exiled 4.2.3

Welcome, this Plugin add SCP-5000 with many features. Fully editable in the Config.

**Features** \
_SpawnChance_ \
_SpawnBroadcast_ \
_SpawnCassie_ \
_SpawnRoom_ \
_Inventory_ \
_Role_ \
_EnableEffect_ \
_Badge Color & Name_ \
_Hp_ \
_TeslaTriggerable_\
_TeslaBroadcast_\
_FemurBreakerTriggerable_\
_FemurBreakerBroadcast_\
_ExplosionCassie_\
_FuseTime_\
_ExplosionNumber_\
_RecontainCassie_\
_AddingTarget_\
_AddingTargetBroadcast_ 

## Config

| Name  | Type | Description | 
| ------------- | ------------- | ------------- |
| IsEnabled  | bool  | Enable or Disable the plugin. |
| EnableCassie  | bool  | Enable or Disable cassie. |
| SpawnChance | int | SCP-5000 spawn chance. |
| SpawnBroadcast | string | Broadcast when you spawn as SCP-5000. |
| SpawnCassie  | string  | when you spawn as SCP-5000. |
| SpawnRoom  | RoomType  | Choose where spawn as SCP-5000. |
| Inventory  | ItemType  | Inventory of SCP-5000. |
| Role  | RoleType  | What role have to be picked when the round start and spawned as SCP-5000. |
| EnableEffect  | bool  | Enable or Disable the Player Effect. |
| Color  | string  | Set the badge color.  |
| Badge  | string  | Set the badge name  |
| Hp  | int | Health amount of SCP-5000  |
| TeslaTriggerable  | bool | Set if SCP-5000 can trigger Tesla Gate. |
| TeslaBroadcast | string | Broadcast when SCP-5000 go near a Tesla Gate.|
| FemurBreakerTriggerable | bool | Set if SCP-5000 can trigger Femur Breaker event. |
| FemurBreakerBroadcast  | string  | Set the Broadcast when SCP-5000 attempt to enter the Femur Breaker.  |
| ExplosionCassie  | string  | Cassie when SCP-5000 automatic destruction start.  |
| FuseTime  | float | Set the fuse delay of ExplosionNumber.  |
| ExplosionEnable  | bool | Enable or Disable if SCP-5000 will explode when he's dying.  |
| ExplosionNumber  | int | ExplosionNumber is the quantity of explosions. |
| RecontainCassie | string | Cassie when SCP-5000 has been recontained.|
| AddingTarget | bool | Set if SCP-5000 can trigger SCP-096. |
| AddingTargetBroadcast  | string | Set the Broadcast when SCP-5000 protect you from become a target of SCP-096. |
  
## Command

| Commands  | Args | Permission | Description | 
| ------------- | ------------- | ------------- | ------------- |
| scp5000  | id/name  | scp5000.spawn | Spawn a player as SCP-5000 |
| killscp5000  | id/name  | scp5000.kill | Kill a player as SCP-5000 |

  
## API
- **TrySpawnScp5000**: Spawn a Player as SCP-5000
- **TryKillSScp5000**: Kill a Player as SCP-5000
- **Players**: type Hashset<Player> (Players as SCP-5000)
- **IsScp5000**: Check if Player is SCP-5000

If you found bug please contact me on discord: **ꜱᴍᴜꜱɪ ᴊᴀʀᴠɪꜱ#5666**

  
## Release
  https://github.com/SmusiJarviss/SCP-5000/releases/tag/1.1.1
