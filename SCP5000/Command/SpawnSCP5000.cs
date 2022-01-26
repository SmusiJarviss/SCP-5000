using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace SCP5000.Command
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Spawn : ICommand
    {
        public string Command { get; } = "scp5000";

        public string[] Aliases { get; } = new[] { "s5000" };

        public string Description { get; } = "Spawn SCP-5000, usage: scp5000 <player name or id>";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("scp5000.spawn"))
            {
                response = "You don't have permission to execute this command.";
                return false;
            }

            if (arguments.Count != 1)
            {
                response = "Usage: scp5000 <player name or id>";
                return false;
            }

            if (Player.Get(arguments.At(0)) is Player player)
            {
                if (!API.SCP5000API.TrySpawnSCP5000(player))
                {
                    response = $"{player.Nickname} Has been already spawned as SCP-5000!";
                    return false;
                }

                API.SCP5000API.TrySpawnSCP5000(player);
                response = $"{player.Nickname} Has been spawned as SCP-5000";
                return true;
            }

            response = "Player not found";
            return false;
        }
    }
}