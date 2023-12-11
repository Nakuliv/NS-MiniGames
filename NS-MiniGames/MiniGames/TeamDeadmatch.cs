using InventorySystem.Items;
using InventorySystem;
using Mirror;
using NS_MiniGames.API;
using PluginAPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEC;
using PlayerRoles;

namespace NS_MiniGames.MiniGames
{
    internal class TeamDeadmatch : MiniGame
    {
        public override string Name { get; set; } = "Team DeathMatch";
        public override int Id { get; set; } = 1;
        public override string Description { get; set; } = "";

        public override void Start()
        {
            var players = Extensions.GetRandomPlayers(Server.Count / 2);
            foreach(var player in players)
            {
                player.SetRole(PlayerRoles.RoleTypeId.Scientist);
            }
            var restplayers = Player.GetPlayers().Except(players);

            foreach(var player in restplayers)
            {
                player.SetRole(PlayerRoles.RoleTypeId.ClassD);
            }

            Plugin.Instance.coroutines.Add(Timing.RunCoroutine(AlivePlayersCheck()));
        }

        private IEnumerator<float> AlivePlayersCheck()
        {
            for (; ; )
            {
                if (!Player.GetPlayers().Any(x => x.Role != RoleTypeId.Spectator || x.Role != RoleTypeId.None))
                {
                    Timing.CallDelayed(3f, () =>
                    {
                        Round.Restart();
                    });
                }

                Server.SendBroadcast("<b><color=#005EFF>T</color><color=#0B58FF>e</color><color=#1652FF>a</color><color=#214CFF>m</color> <color=#3740FF>D</color><color=#423AFF>e</color><color=#4D34FF>a</color><color=#582EFF>t</color><color=#6328FF>h</color><color=#6E22FF>m</color><color=#791CFF>a</color><color=#8416FF>t</color><color=#8F10FF>c</color><color=#9A0AFF>h</color></b>\n" +
                    $"<size=30>{Player.GetPlayers().Where(x=>x.Role==RoleTypeId.Scientist).Count()} | {Player.GetPlayers().Where(x=>x.Role == RoleTypeId.ClassD).Count()}\n" +
                    "<color=yellow>naukowcy</color> <color=orange>klasa D</color></size>", 3);

                yield return Timing.WaitForSeconds(3.1f);
            }


        }
    }
}
