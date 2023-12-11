using MEC;
using NightStars_Plugin.API;
using NightStars_Plugin.API.Dummy;
using NS_MiniGames.API;
using PlayerRoles;
using PluginAPI.Core;
using PluginAPI.Enums;
using PluginAPI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NS_MiniGames.Hats;
using UnityEngine;
using AdminToys;

namespace NS_MiniGames
{
    public class Plugin
    {
        public static Plugin Instance { get; private set; }
        public Dictionary<string,int> Votes = new Dictionary<string,int>();
        public Dictionary<int, MiniGame> MiniGames = new Dictionary<int, MiniGame>();
        public Dictionary<int, Hat> Hats = new Dictionary<int, Hat>();
        public PrimitiveObjectToy primitiveObjectPrefab;

        public List<CoroutineHandle> coroutines = new List<CoroutineHandle>();
        Npc dummy;

        [PluginAPI.Core.Attributes.PluginEntryPoint("NightStars MiniGames", "1.0.0", "", "Nakuliv")]
        private void Load()
        {
            Instance = this;

            EventManager.RegisterEvents(this);
            new ExampleHat();
        }

        [PluginAPI.Core.Attributes.PluginEvent(ServerEventType.WaitingForPlayers)]
        private void OnWaitingForPlayers()
        {
            dummy = Npc.Create(Vector3.one, pos: new RoomPoint() { roomName="Outside", position = new Vector3(141.1269f, 0.5350952f, -45.3059f)}.GetMapPosition());
            dummy.SetRotation(Quaternion.Euler(0f, 0f, -90f));
            coroutines.Add(Timing.RunCoroutine(WaitingForStart()));
        }

        [PluginAPI.Core.Attributes.PluginEvent(ServerEventType.RoundStart)]
        private void roundstart()
        {
            dummy.Hub.roleManager.ServerSetRole(RoleTypeId.Tutorial, RoleChangeReason.RemoteAdmin);
            dummy.SetPosition(new RoomPoint() { roomName = "Outside", position = new Vector3(141.1269f, 0.5350952f, -45.3059f) }.GetMapPosition());
            dummy.SetRotation(Quaternion.Euler(0f, -90f, 0f));
        }

        [PluginAPI.Core.Attributes.PluginEvent(ServerEventType.RoundRestart)]
        private void OnRoundRestart()
        {
            Timing.KillCoroutines((coroutines.ToArray()));
        }

        private IEnumerator<float> WaitingForStart()
        {
            for (; ; )
            {
                string s =
                    "<b><color=#250FF0>W</color><color=#241AF0>i</color><color=#2325F0>t</color><color=#2230F0>a</color><color=#213BF0>j</color> <color=#1F51F0>n</color><color=#1E5CF0>a</color> <color=yellow>⭐</color><color=#1B7DF0>N</color><color=#1A88F0>i</color><color=#1993F0>g</color><color=#189EF0>h</color><color=#17A9F0>t</color><color=#16B4F0>S</color><color=#15BFF0>t</color><color=#14CAF0>a</color><color=#13D5F0>r</color><color=#12E0F0>s</color><color=yellow>⭐</color></b>\" +\r\n                    \"\\n<size=30><b><color=#5C4CE9>Z</color><color=#5B52E8>a</color><color=#5A58E7>g</color><color=#595EE6>ł</color><color=#5864E5>o</color><color=#576AE4>s</color><color=#5670E3>u</color><color=#5576E2>j</color> <color=#5382E0>n</color><color=#5288DF>a</color> <color=#5094DD>m</color><color=#4F9ADC>i</color><color=#4EA0DB>n</color><color=#4DA6DA>i</color><color=#4CACD9>g</color><color=#4BB2D8>r</color><color=#4AB8D7>e</color></b></size>";
                /*foreach (var ev in Plugin.Instance.MiniGames)
                {
                    s += $"\n{ev.Value.Name} [Głosy: ]";
                }*/
                Server.SendBroadcast(s, 3);
                yield return Timing.WaitForSeconds(3.1f);
            }


        }

        [PluginAPI.Core.Attributes.PluginConfig]
        public Config Config;
    }
}
