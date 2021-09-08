using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using System.Linq;

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
                response = "Missing Permission.";
                return false;
            }

            if (arguments.Count != 1)
            {
                response = "Usage: scp5000 <player name or id>";
                return false;
            }

            Player player = null;

            if (int.TryParse(arguments.ElementAt(0), out int id))
            {
                player = Player.Get(id);

                if (player is null)
                {
                    response = "Player not found.";
                    return false;
                }

                if (API.API.Players.Contains(player))
                {
                    response = "Player already SCP-5000";
                    return false;
                }

                API.API.SpawnSCP5000(player);
            }

            response = $"{player.Nickname} spawned as SCP-5000.";
            return true;
        }
    }
}