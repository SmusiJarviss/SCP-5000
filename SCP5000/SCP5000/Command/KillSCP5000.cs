using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace SCP5000.Command
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Kill : ICommand
    {
        public string Command { get; } = "killscp5000";

        public string[] Aliases { get; } = new[] { "ks5000" };

        public string Description { get; } = "Kill SCP-5000, usage: killscp5000 <player name or id>";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("scp5000.kill"))
            {
                response = "You don't have permission to execute this command.";
                return false;
            }

            if (arguments.Count != 1)
            {
                response = "Usage: killscp5000 <player name or id>";
                return false;
            }

            if (Player.Get(arguments.At(0)) is Player player)
            {
                if (!API.SCP5000API.Players.Contains(player))
                {
                    response = $"{player.Nickname} is not SCP-5000!";
                    return false;
                }

                player.SetRole(RoleType.Spectator);
                API.SCP5000API.KillSCP5000(player);
                API.SCP5000API.Players.Remove(player);
                response = $"{player.Nickname} Has no longer SCP-5000";
                return true;
            }
            response = "Player not found";
            return false;
        }
    }
}