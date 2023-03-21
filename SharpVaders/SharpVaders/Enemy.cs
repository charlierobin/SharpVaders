using System;
using CoreGraphics;
using Foundation;
using SpriteKit;

namespace SharpVaders
{
    public class Enemy : SKSpriteNode
    {
        public int value = -1;

        private SceneGame game;

        private Wave wave;

        public nfloat startX;
        public nfloat startY;

        private double minTimeIntervalInSeconds = 5;
        private double maxTimeIntervalInSeconds = 25;

        public Enemy(SceneGame game, Wave wave, nfloat x, nfloat y, int value, string texture) : base(texture: SKTexture.FromImageNamed(NSBundle.MainBundle.PathForResource(texture, "png")))
        {
            this.game = game;
            this.wave = wave;

            this.Name = "Enemy";
            this.value = value;

            this.PhysicsBody = SKPhysicsBody.CreateCircularBody(400);
            this.PhysicsBody.AffectedByGravity = false;

            this.PhysicsBody.CategoryBitMask = (uint)Types.Enemy;
            this.PhysicsBody.CollisionBitMask = (uint)Types.Nothing;
            this.PhysicsBody.ContactTestBitMask = (uint)Types.Shield + (uint)Types.Player;

            this.SetScale(0.08f);

            this.Position = new CGPoint(x, y);

            this.startX = x;
            this.startY = y;

            this.game.Add(this);

            this.cueNextBombDrop();
        }

        public virtual void hit()
        {
            this.wave.hit(this);
        }

        private void cueNextBombDrop()
        {
            this.RunAction(SKAction.WaitForDuration(this.game.random.Next(this.minTimeIntervalInSeconds, this.maxTimeIntervalInSeconds)), () =>
            {
                this.game.Add(new EnemyBullet(this));

                this.cueNextBombDrop();
            });
        }
    }
}

