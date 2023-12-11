using CommandSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginAPI.Core;
using NS_MiniGames.API;

namespace NS_MiniGames.Commands.Voting
{
    [CommandHandler(typeof(ClientCommandHandler))]
    internal class Vote : ICommand
    {
        public string Command => "vote";

        public string[] Aliases => new string[] { "vt" };

        public string Description => "głosowanie na event ns";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player ply = Player.Get(sender);

            if(Plugin.Instance.Votes.ContainsKey(ply.UserId))
            {
                response = $"<color=red><b>Możesz zagłosować tylko raz!</b></color>";
                return true;
            }

            Plugin.Instance.Votes.Add(ply.UserId, arguments.At(0));
            response = $"<b>Zagłosowano na <color=blue></color></b>";
            return true;
        }
    }
}
