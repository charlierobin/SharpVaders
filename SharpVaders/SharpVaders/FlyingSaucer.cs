using System;
using System.Timers;
using CoreGraphics;
using Foundation;
using JavaScriptCore;
using SpriteKit;

namespace SharpVaders
{
    public class FlyingSaucer : SKSpriteNode
    {
        private static SKAction leftToRight;
        private static SKAction rightToLeft;

        public int value = -1;

        private SceneGame game;
        private FlyingSaucers controller;

        private float timeToCrossScreen = 5;

        private double minTimeIntervalInSeconds = 0.5f;
        private double maxTimeIntervalInSeconds = 6;

        public FlyingSaucer(SceneGame game, FlyingSaucers controller) : base(texture: SKTexture.FromImageNamed(NSBundle.MainBundle.PathForResource("graphics/enemies/saucer", "png")))
        {
            this.game = game;
            this.controller = controller;

            this.Name = "FlyingSaucer";

            this.value = 1000;

            this.PhysicsBody = SKPhysicsBody.CreateCircularBody(500);
            this.PhysicsBody.AffectedByGravity = false;

            this.PhysicsBody.CategoryBitMask = (uint)Types.FlyingSaucer;
            this.PhysicsBody.CollisionBitMask = (uint)Types.Nothing;

            this.SetScale(0.08f);

            this.game.Add(this);

            if (FlyingSaucer.leftToRight == null)
            {
                FlyingSaucer.leftToRight = SKAction.Sequence(SKAction.MoveToX(this.game.Frame.Width + this.CalculateAccumulatedFrame().Width, this.timeToCrossScreen), SKAction.RemoveFromParent());

                FlyingSaucer.rightToLeft = SKAction.Sequence(SKAction.MoveToX(-this.CalculateAccumulatedFrame().Width, this.timeToCrossScreen), SKAction.RemoveFromParent());
            }

            SKAction action;

            if (this.game.random.NextBoolean())
            {
                this.Position = new CGPoint(-this.CalculateAccumulatedFrame().Width, this.game.Frame.Height - 40);

                action = FlyingSaucer.leftToRight;
            }
            else
            {
                this.Position = new CGPoint(this.game.Frame.Width + this.CalculateAccumulatedFrame().Width, this.game.Frame.Height - 40);

                action = FlyingSaucer.rightToLeft;
            }

            this.RunAction(action, () =>
            {
                this.controller.saucerDestroyed();
            });

            this.cueNextBombDrop();
        }

        public void hit()
        {
            this.controller.saucerDestroyed();
        }

        private void cueNextBombDrop()
        {
            this.RunAction(SKAction.WaitForDuration(this.game.random.Next(this.minTimeIntervalInSeconds,this.maxTimeIntervalInSeconds)), () =>
            {
                this.game.Add(new EnemyBullet(this));

                this.cueNextBombDrop();
            });
        }
    }
}

