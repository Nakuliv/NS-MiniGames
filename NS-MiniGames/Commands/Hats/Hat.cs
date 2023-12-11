using CommandSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NS_MiniGames.API;
using PluginAPI.Core;
using Mirror;
using UnityEngine;
using AdminToys;

namespace NS_MiniGames.Commands.Hats
{
    [CommandHandler(typeof(ClientCommandHandler))]
    internal class SpawnHat : ICommand
    {
        public string Command => "hat";

        public string[] Aliases => new string[] { "ht" };

        public string Description => "Wybierz czapkę";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!arguments.Any())
            {
                Log.Info("test0");
                response = "<color=red>Musisz podać nazwę czapki!</color>";
                return true;
            }

            if (arguments.Count > 1)
            {
                Log.Info("test4");
                response = "<color=red>Zła nazwa czapki!</color>";
                return true;
            }

            if (Plugin.Instance.Hats.TryGetValue(int.Parse(arguments.At(0)), out Hat hat))
            {
                Log.Info("test");
                var ply = Player.Get(sender);
                var primitive = UnityEngine.Object.Instantiate(NetworkClient.prefabs.FirstOrDefault(x => x.Value.GetComponent<PrimitiveObjectToy>()).Value.GetComponent<PrimitiveObjectToy>());
                primitive.NetworkPrimitiveType = PrimitiveType.Cube;

                primitive.transform.position = ply.Position + Extensions.GetHatPosForRole(ply.Role);
                primitive.transform.localScale = new Vector3(0.5f, 0.1f, 0.5f);
                primitive.Scale = new Vector3(0.5f, 0.1f, 0.5f);
                var scale = primitive.Scale;
                primitive.transform.localScale = new Vector3(-Math.Abs(scale.x), -Math.Abs(scale.y), -Math.Abs(scale.z));
                primitive.Scale = new Vector3(-Math.Abs(scale.x), -Math.Abs(scale.y), -Math.Abs(scale.z));
                primitive.NetworkMaterialColor = Color.blue;
                primitive.NetworkMovementSmoothing = 60;
                Log.Info("test2");
                NetworkServer.Spawn(primitive.gameObject);
                hat.Object = primitive.gameObject;
                hat.Owner = ply;
                hat.StartFollowing();
                Log.Info("test3");
                response = "<color=blue>Dodano czapkę!</color>";
                return true;
            }
            else
            {
                Log.Info("test5");
                response = "<color=red>Zła nazwa czapki!</color>";
                return true;
            }
        }
    }
}
