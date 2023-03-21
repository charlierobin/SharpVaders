using System;
using SpriteKit;

namespace SharpVaders
{
    public class Shields
    {
        private SceneGame game;

        public Shields(SceneGame game)
        {
            this.game = game;

            this.refreshShields();
        }

        public void refreshShields()
        {
            this.game.EnumerateChildNodes("Shield", (SKNode node, out bool stop) =>
            {
                node.RemoveFromParent();

                stop = false;
            });

            this.game.Add(new Shield(this.game, 150, 140));

            this.game.Add(new Shield(this.game, 500, 140));

            this.game.Add(new Shield(this.game, 850, 140));

            this.game.Add(new Shield(this.game, 1200, 140));

            this.game.Add(new Shield(this.game, 1500, 140));
        }
    }
}

