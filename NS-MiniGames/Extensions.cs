using PlayerRoles;
using PluginAPI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NS_MiniGames
{
    public static class Extensions
    {
        public static List<Player> GetRandomPlayers(int count) => Player.GetPlayers().OrderBy(arg=>Guid.NewGuid()).Take(count).ToList();
        public static Player GetRandomPlayers() => Player.GetPlayers().RandomItem();

        public static Vector3 GetHatPosForRole(RoleTypeId role)
        {
            switch (role)
            {
                case RoleTypeId.Scp173:
                    return new Vector3(0, .55f, -.05f);
                case RoleTypeId.Scp106:
                    return new Vector3(0, .45f, .18f);
                case RoleTypeId.Scp096:
                    return new Vector3(.15f, .425f, .325f);
                case RoleTypeId.Scp939:
                    return new Vector3(0, -.5f, 1.125f);
                case RoleTypeId.Scp049:
                    return new Vector3(0, .125f, -.05f);
                case RoleTypeId.None:
                    return new Vector3(-1000, -1000, -1000);
                case RoleTypeId.Spectator:
                    return new Vector3(-1000, -1000, -1000);
                case RoleTypeId.Scp0492:
                    return new Vector3(0, .1f, -.16f);
                default:
                    return new Vector3(0, .15f, -.07f);
            }
        }
    }
}
