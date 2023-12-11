using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEC;
using PluginAPI.Core;
using UnityEngine;

namespace NS_MiniGames.API
{
    public abstract class Hat
    {
        public abstract GameObject Object { get; set; }
        public CoroutineHandle coroutine;
        public Vector3 Position
        {
            get => Object.transform.position;
            set => Object.transform.position = value;
        }
        public Vector3 Rotation
        {
            get => Object.transform.eulerAngles;
            set => Object.transform.eulerAngles = value;
        }

        public Player Owner { get; set; }
        public abstract int Id { get; set; }

        public Hat()
        {
            Plugin.Instance.Hats.Add(Id, this);
        }

        public void StartFollowing()
        {
            Position = Owner.Camera.position + Extensions.GetHatPosForRole(Owner.Role);
            coroutine = Timing.RunCoroutine(Follow());
        }

        public void StopFollowing()
        {
            Timing.KillCoroutines(coroutine);
        }

        private IEnumerator<float> Follow()
        {
            for (;;)
            {
                Position = Owner.Camera.position + Extensions.GetHatPosForRole(Owner.Role);
                Rotation = Owner.Rotation;
                yield return Timing.WaitForOneFrame;
            }
        }
    }
}
