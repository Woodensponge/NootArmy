using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NootArmy
{
    class Game
    {
        public RainWorld instance;
        public RainWorldGame rainWorldGame;
        public Player player;

        public Game()
        {
            this.instance = UnityEngine.Object.FindObjectOfType<RainWorld>();
            this.rainWorldGame = instance.processManager.currentMainLoop as RainWorldGame;
            this.player = (rainWorldGame.Players.FirstOrDefault<AbstractCreature>().realizedCreature as Player);
        }
    }
}
