using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS_MiniGames.API
{
    public abstract class MiniGame
    {
        public abstract string Name { get; set; }
        public abstract int Id { get; set; }
        public abstract string Description { get; set; }

        public MiniGame()
        {
            Plugin.Instance.MiniGames.Add(Id, this);
        }

        public abstract void Start();
    }
}
