using System;
using CoreGraphics;
using Foundation;
using SceneKit;
using SpriteKit;

namespace SharpVaders
{
    public class FlyingSaucers : SKNode
    {
        private double minTimeIntervalInSeconds = 2;
        private double maxTimeIntervalInSeconds = 5;

        private SceneGame game;

        public FlyingSaucers(SceneGame game)
        {
            this.Name = "FlyingSaucers";

            this.game = game;

            this.cueNextSpawn();
        }

        public void saucerDestroyed()
        {
            this.cueNextSpawn();
        }

        private void spawn()
        {
            FlyingSaucer saucer = new FlyingSaucer(this.game, this);
        }

        private void cueNextSpawn()
        {
            this.RunAction(SKAction.WaitForDuration(this.game.random.Next(this.minTimeIntervalInSeconds, this.maxTimeIntervalInSeconds)), () =>
            {
                this.spawn();
            });
        }
    }
}

