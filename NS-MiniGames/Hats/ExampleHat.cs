using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NS_MiniGames.API;
using PluginAPI.Core;
using UnityEngine;

namespace NS_MiniGames.Hats
{
    internal class ExampleHat : Hat
    {
        public override GameObject Object { get; set; }
        public override int Id { get; set; } = 0;
    }
}
